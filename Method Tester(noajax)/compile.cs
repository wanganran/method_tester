using System;
using System.Data;
using System.Configuration;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using System.CodeDom.Compiler;
using Microsoft.CSharp;


namespace MethodTester
{
    class CompilerUnit
    {
        /// <summary>
        /// 获取或设置 编译函数代码时引用的程序集的完整路径的集合
        /// </summary>
        public static System.Collections.Specialized.StringCollection ReferenceAssemblyCollection = new System.Collections.Specialized.StringCollection();

        //private static ICodeCompiler compiler =  (new VBCodeProvider()).CreateCompiler();


        /// <summary>
        /// 编译
        /// </summary>
        /// <param name="source">源代码</param>
        /// <param name="makeExe">是否生成可执行程序</param>
        /// <param name="inMemory">是否在内存中生成,(如果true请将 outputAssembly设置为null)</param>
        /// <param name="dlls">加载的dll的完整路径</param>
        /// <param name="outputAssembly">输出程序集的完整路径(如果inMemory参数为true则请使用null)</param>
        public static CompilerResults Compile(string source, bool makeExe, bool inMemory, string[] dlls, string outputAssembly)
        {
            CompilerParameters parameters = new CompilerParameters();
            
            //加载dll
            if (dlls != null)
            {
                parameters.ReferencedAssemblies.AddRange(dlls);
            }
            parameters.GenerateExecutable = makeExe;
            parameters.GenerateInMemory = inMemory;
            if (!inMemory)
            {
                parameters.OutputAssembly = outputAssembly;
            }

            //CompilerResults results = compiler.CompileAssemblyFromSource(parameters, source);
            CompilerResults results = (new CSharpCodeProvider()).CompileAssemblyFromSource(parameters, source);
            return results;
        }



    }
}
