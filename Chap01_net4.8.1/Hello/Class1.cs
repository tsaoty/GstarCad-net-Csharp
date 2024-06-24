/*
#if !HC2020
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Acap = Autodesk.AutoCAD.ApplicationServices.Application;
#else
*/

using GrxCAD.ApplicationServices;
using GrxCAD.Runtime;
using GrxCAD.EditorInput;
using Acap = GrxCAD.ApplicationServices.Application;
//#endif

namespace ClassLibrary1
{
    public class Class1
    {
        [CommandMethod("ty_Hello")]
        public void Hello()
        {
            // 获取当前活动文档的Editor对象，也就是命令行
            Editor ed=Application.DocumentManager.MdiActiveDocument.Editor;
            // 调用Editor对象的WriteMessage函数在命令行上显示文本
            ed.WriteMessage("tty gCad msg> 欢迎进入.NET开发AutoCAD的世界！");
        }
    }
}
