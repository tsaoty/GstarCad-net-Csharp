using GrxCAD.Runtime;
using GrxCAD.DatabaseServices;
using GrxCAD.Geometry;
using DotNetARX;

namespace Lines
{
    public class Lines
    {
        [CommandMethod("ty_FirstLine")]
        public static void FirstLine()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Point3d startPoint = new Point3d(0, 100, 0);
            Point3d endPoint = new Point3d(100, 100, 0);
            Line line = new Line(startPoint, endPoint);
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                btr.AppendEntity(line);
                trans.AddNewlyCreatedDBObject(line, true);
                trans.Commit();
            }
        }

        [CommandMethod("ty_SecondLine")]
        public static void SecondLine()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Point3d startPoint = new Point3d(0, 100, 0);
            double angle = GeTools.DegreeToRadian(90);
            Line line = new Line(startPoint, startPoint.PolarPoint(angle, 100));
            db.AddToModelSpace(line);
        }
    }
}