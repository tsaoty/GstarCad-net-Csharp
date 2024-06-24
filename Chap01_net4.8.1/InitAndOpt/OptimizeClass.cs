/*
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
*/

using GrxCAD.ApplicationServices;
using GrxCAD.Runtime;
using GrxCAD.EditorInput;
using GrxCAD.DatabaseServices;
using Acap = GrxCAD.ApplicationServices.Application;
//using Gssoft.Gscad.Runtime;

namespace InitAndOpt
{
    public class OptimizeClass
    {
        [GrxCAD.Runtime.CommandMethod("ty_OptCommand")]
        public void OptCommand()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            string fileName = "C:\\Hello.dll";  // Hello.dll程序集的文件名
            try
            {
                //ExtensionLoader.Load(fileName); // 载入Hello.dll程序集
                //Gssoft.Gscad.Runtime.ExtensionLoader.Load(fileName);
                // 在命令行上显示信息，提示用户Hello.dll程序集已经被载入
                ed.WriteMessage("\n tty gCad msg> " + fileName + " 被载入，请输入ty_Hello 进行测试！");
            }
            catch (System.Exception ex) //補捉程式異常
            {
                ed.WriteMessage("\ntty gCad msg>" + ex.Message); //顯示異常信息
            }
            finally
            {
                ed.WriteMessage("\ntty gCad msg> Finally：程式執行完畢！");
            }
        }
        [CommandMethod("ty_ChangeColor")]
        public void ChangeColor()
        {
            //Database db = Acap.DocumentManager.MdiActiveDocument.Database;
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Acap.DocumentManager.MdiActiveDocument.Editor;
            try
            {

            }
            catch
            {

            }
        }
    }
}
