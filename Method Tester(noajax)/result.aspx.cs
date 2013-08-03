using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MethodTester;
using System.Collections.Generic;
using System.Reflection;
using Squishyware.Text.SyntaxHighlighting;

namespace Method_Tester
{
    public partial class result : System.Web.UI.Page
    {
        public struct progcode
        {
            public string[] UsingList;
            public string MethodName;
            public string Param;
            public string MethodCode;
            public string OtherCode;
            public string Description;

        }
        public progcode current;
        protected void Page_Load(object sender, EventArgs e)
        {
            getcurrent();
            if (Request["action"] != null && Request["action"] == "format")
            {
                string s = GetSource();
                Response.Write("<html><head></head><body><div>");
                var a = CSharpHighlighter.GetHighlighter(SyntaxType.CSharp);
                Response.Write(a.Highlight(s));
                Response.Write("</div></body></html>");
                Response.End();
            }
            lblProgname.Text=current.MethodName;

            string[] errmsg;
            string[] warningmsg;
            string runtimemsg;
            string res = Execute(out errmsg, out warningmsg, out runtimemsg);
            if (errmsg != null && errmsg.Length != 0)
                lblFlag.Text = "Sorry, there're some errors when compiling.";
            else if (runtimemsg != null && runtimemsg.Length != 0)
                lblFlag.Text = "Sorry, there're some errors when running.";
            else if (warningmsg != null && warningmsg.Length != 0)
                lblFlag.Text = "Compiling OK, but there're some warnings.";
            else
                lblFlag.Text = "Compiling OK.";
            divMessages.InnerHtml = "<h3>Compiling errors:"+(errmsg==null?"0":errmsg.Length.ToString())+"</h3>";
            foreach (string msg in errmsg)
                divMessages.InnerHtml += "<p>" + Server.HtmlEncode(msg) + "</p>";
            divMessages.InnerHtml += "<h3>Compiling warnings:" +(warningmsg==null?"0": warningmsg.Length.ToString()) + "</h3>";
            foreach (string msg in warningmsg)
                divMessages.InnerHtml += "<p>" +Server.HtmlEncode( msg) + "</p>";
            divMessages.InnerHtml += "<h3>Runtime errors:"+(runtimemsg==null?"none":"")+"</h3>";
            divMessages.InnerHtml += "<p>" + Server.HtmlEncode(runtimemsg) + "</p>";
            if (res != null)
                divResult.InnerHtml = Server.HtmlEncode(res).Replace("\n", "<br/>");
            else
                divResult.InnerHtml = "";
        }

        private void getcurrent()
        {
            try
            {
                current = new progcode();
                current.MethodName = Request["txtMethodname"];
                current.MethodCode = Request["txtMainmethod"];
                current.Description = Request["txtDescription"];
                current.OtherCode = Request["txtOthercode"];
                current.Param = Request["param"];
                string ul = Request["txtUsinglist"];
                ul.Replace("\r", "");
                current.UsingList = ul.Split('\n');
            }
            catch
            {
                Response.Write("Please write any code first.");
                Response.End();
            }
        }
        string GetSource()
        {
            string res = "/*\r\n";
            res += current.Description;
            res += "*/\r\n";
            foreach (string u in current.UsingList)
            {
                res += "using " + u + ";\r\n";
            }
            res += "namespace ns{\r\n";
            res += "public class test{\r\n";
            res += current.OtherCode + "\r\n";
            res += "public static string ";
            res += current.MethodName;
            res += "(string args){\r\n";
            res += current.MethodCode;
            res += "\r\n}\r\n}\r\n}";

            return res;
        }
        string Execute(out string[] errormessages,out string[] warningmessages,out string runtimemessages)
        {
            var res = CompilerUnit.Compile(GetSource(), false, true, new string[] { "System.dll" }, null);
            //messages = new string[0];
            List<string> err = new List<string>();
            List<string> warning = new List<string>();
                
            if (res.Errors.HasErrors || res.Errors.HasWarnings)
            {
                foreach (System.CodeDom.Compiler.CompilerError error in res.Errors)
                {
                    if (error.IsWarning) warning.Add(error.Line.ToString() + " 行 " + error.ErrorNumber.ToString() + " " + error.ErrorText);
                    else err.Add(error.Line.ToString() + " 行 " + error.ErrorNumber.ToString() + " " + error.ErrorText);
                }
            }
            errormessages = err.ToArray();
            warningmessages = warning.ToArray();

            if (!res.Errors.HasErrors)
            {

                Assembly ass = res.CompiledAssembly;
                Type tp = ass.GetType("ns.test");
                MethodInfo mi = tp.GetMethod(current.MethodName);
                Object obj = System.Activator.CreateInstance(tp);
                try
                {
                    object result = mi.Invoke(obj, new object[] { current.Param });
                    string res2 = result.ToString();
                    runtimemessages = null;
                    return res2;
                }
                catch (Exception ex)
                {

                    runtimemessages = ex.Message;
                }

            }
            
            runtimemessages = null;
            return null;
        }
    }
}
