using GrxCAD.ApplicationServices;
using GrxCAD.Runtime;
using GrxCAD.EditorInput;

[assembly: ExtensionApplication(typeof(myInitAndOpt2.InitClass))]
//[assembly: CommandClass(typeof(myInitAndOpt2.OptimizeClass))]

namespace myInitAndOpt2
{
    public class InitClass : IExtensionApplication
    {
        public void Initialize()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            // 在AutoCAD命令行上显示一些信息，它们会在程序载入时被显示
            ed.WriteMessage("tty_程序开始初始化。");
        }

        public void Terminate()
        {
            // 在Visual Studio 2010的输出窗口上显示程序结束的信息
            System.Diagnostics.Debug.WriteLine(
               "tty_程序结束，你可以在里做一些程序的清理工作，如关闭AutoCAD文档");
        }
        
        [CommandMethod("InitCommand")]
        public void InitCommand()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("myInitAndOpt2_Test");
        }
    }
 }