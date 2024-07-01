using GrxCAD.Runtime;
using GrxCAD.DatabaseServices;
using GrxCAD.Geometry;
using System;

namespace Lines
{
    public static class GeTools
    {
        // 計算兩個點的中點
        public static Point3d MidPoint(Point3d pt1, Point3d pt2)
        {
            return new Point3d((pt1.X + pt2.X) / 2, (pt1.Y + pt2.Y) / 2, (pt1.Z + pt2.Z) / 2);
        }
        // 將極座标轉換為Point3d對象
        public static Point3d PolarPoint(this Point3d point, double angle, double dist)
        {
            return new Point3d(point.X + dist * Math.Cos(angle), point.Y + dist * Math.Sin(angle), point.Z);
        }
        // 角度转弧度
        public static double DegreeToRadian(double degree)
        {
            return degree * Math.PI / 180;
        }
    }

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

        public static ObjectId AddToModelSpace(Database DBIn, Entity EntityIn)
        {
            using (Transaction myTrans = DBIn.TransactionManager.StartTransaction())
            {
                BlockTable myBT = (BlockTable)myTrans.GetObject(DBIn.BlockTableId, OpenMode.ForRead);
                BlockTableRecord myModelSpace = (BlockTableRecord)myTrans.GetObject(myBT[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                myModelSpace.AppendEntity(EntityIn);
                myTrans.AddNewlyCreatedDBObject(EntityIn, true);
                myTrans.Commit();
                return EntityIn.ObjectId;
            }
        }

        [CommandMethod("ty_SecondLine")]
        public static void SecondLine()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Point3d startPoint = new Point3d(0, 100, 0);
            double angle = GeTools.DegreeToRadian(120);
            Line line = new Line(startPoint, startPoint.PolarPoint(angle, 100));
            AddToModelSpace(db, line);
        }
    }
}