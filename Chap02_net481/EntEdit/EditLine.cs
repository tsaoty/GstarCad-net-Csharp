using GrxCAD.ApplicationServices;
using GrxCAD.Runtime;
using GrxCAD.DatabaseServices;
using GrxCAD.Geometry;
using GrxCAD.EditorInput;
using System;
using DotNetARX;

namespace EntEdit
{
    public class EditLine
    {
        [CommandMethod("ty_EditLine")]
        public static void EditMyLine()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            GrxCAD.DatabaseServices.TransactionManager tm = db.TransactionManager;
            //第一事務處理
            using (Transaction tr1=tm.StartTransaction())
            {
                Point3d ptStart = Point3d.Origin;
                Point3d ptEnd = new Point3d(100, 0, 0);
                Line line1 = new Line(ptStart, ptEnd);
                ObjectId id1 = db.AddToModelSpace(line1);
                //第二事務處理
                using (Transaction tr2=tm.StartTransaction())
                {
                    line1.UpgradeOpen();
                    line1.ColorIndex = 1; //red
                    ObjectId id2 = id1.Copy(ptStart, ptEnd);
                    id2.Rotate(ptEnd, Math.PI / 2);  //轉90度
                    //第三事務處理
                    using (Transaction tr3 = tm.StartTransaction())
                    {
                        Line line2 = (Line)tr3.GetObject(id2, OpenMode.ForWrite);
                        line2.ColorIndex = 3; //green
                        tr3.Abort();
                    }
                    tr2.Commit();
                }
                tr1.Commit();
            }
        }
    }
}
