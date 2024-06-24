﻿/*
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Acap = Autodesk.AutoCAD.ApplicationServices.Application;
*/

using GrxCAD.ApplicationServices;
using GrxCAD.Runtime;
using GrxCAD.EditorInput;

[assembly: ExtensionApplication(typeof(InitAndOpt.InitClass))]
[assembly: CommandClass(typeof(InitAndOpt.OptimizeClass))]

namespace InitAndOpt
{
    public class InitClass : IExtensionApplication
    {
        public void Initialize()
        {
            Editor ed= Application.DocumentManager.MdiActiveDocument.Editor;
            // 在AutoCAD命令行上显示一些信息，它们会在程序载入时被显示
            ed.WriteMessage("tty gCad msg> 程序开始初始化。");
        }

        public void Terminate()
        {
            // 在Visual Studio 2010的输出窗口上显示程序结束的信息
            System.Diagnostics.Debug.WriteLine(
        "tty gCad msg> 程序结束，你可以在里做一些程序的清理工作，如关闭AutoCAD文档");
        }

        [CommandMethod("ty_InitCommand")]
        public void InitCommand()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("tty dCad msg> Test!");
        }
    }
}
