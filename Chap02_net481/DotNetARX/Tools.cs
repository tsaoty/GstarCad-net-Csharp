using GrxCAD.Runtime;
using GrxCAD.DatabaseServices;
using GrxCAD.Geometry;
using System;

namespace DotNetARX
{
    public static class GeTools
    {
        /// <summary>
        /// 計算兩個點的中點 
        /// </summary>
        /// <param name="pt1">第1點</param>
        /// <param name="pt2">第2點</param>
        /// <returns>點1,2的中點</returns>
        public static Point3d MidPoint(Point3d pt1, Point3d pt2)
        {
            return new Point3d((pt1.X + pt2.X) / 2, (pt1.Y + pt2.Y) / 2, (pt1.Z + pt2.Z) / 2);
        }
        /// <summary>
        /// 將極座标轉換為Point3d物件 
        /// </summary>
        /// <param name="point">點</param>
        /// <param name="angle">角度</param>
        /// <param name="dist">距離</param>
        /// <returns>3d點物件</returns>
        public static Point3d PolarPoint(this Point3d point, double angle, double dist)
        {
            return new Point3d(point.X + dist * Math.Cos(angle), point.Y + dist * Math.Sin(angle), point.Z);
        }
        /// <summary>
        /// 角度轉弧度
        /// </summary>
        /// <param name="degree">角度</param>
        /// <returns>弧度</returns>
        public static double DegreeToRadian(double degree)
        {
            return degree * Math.PI / 180;
        }
    }

    public static class Tools
    {
        /// <summary>
        /// 將實體新增到模型空間
        /// </summary>
        /// <param name="db">數據庫 Database</param>
        /// <param name="ent">要增加的實體</param>
        /// <returns>傳回新增模型空間的實體物件 ObjectId</returns>
        public static ObjectId AddToModelSpace(this Database db, Entity ent)
        {
            ObjectId entId;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                entId =btr.AppendEntity(ent);
                trans.AddNewlyCreatedDBObject(ent, true);
                trans.Commit();
            }
            return entId;
        }
    }
}
