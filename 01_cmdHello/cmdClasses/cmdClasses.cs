using System;

using GrxCAD.Runtime;
using GrxCAD.ApplicationServices;
using GrxCAD.EditorInput;
using GrxCAD.Geometry;
using GrxCAD.DatabaseServices;

[assembly: CommandClass(typeof(cmdClasses.HelloCmd))]
[assembly: CommandClass(typeof(cmdClasses.StaticCmd))]
[assembly: CommandClass(typeof(cmdClasses.LocalCmd))]
[assembly: CommandClass(typeof(cmdClasses.AddlineCmd))]

namespace cmdClasses
{
    public class HelloCmd
    {
        [CommandMethod("ty_Hello")]
        static public void DoIt()
        {
            try
            {
                Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Hello .net C# - tty from VS2019");
            }
            catch (System.Exception ex)
            {
                String str = ex.ToString();
            }
        }
    }

    static public class StaticCmd
    {
        public static int counter = 0;
        [CommandMethod("ty_glob")]
        public static void Global()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\nCounter value is: " + counter++);
        }
    }

    public class LocalCmd
    {
        private int counter = 0;
        [CommandMethod("ty_loc")]
        public void Local()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\nCounter value is: " + counter++);
        }
    }

    public class AddlineCmd
    {
        [CommandMethod("ty_addline")]
        static public void DoIt()
        {
            Point3d st = new Point3d(100.0, 100.0, 0.0);
            Point3d ed = new Point3d(200.0, 200.0, 0.0);
            Line lin = new Line(st, ed);

            Database db = HostApplicationServices.WorkingDatabase;
            Transaction trans = db.TransactionManager.StartTransaction();

            try
            {

                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                btr.AppendEntity(lin);
                trans.AddNewlyCreatedDBObject(lin, true);
                trans.Commit();
            }
            catch (System.Exception ex)
            {
                Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(ex.ToString());
                trans.Abort();
            }
        }
    }
}