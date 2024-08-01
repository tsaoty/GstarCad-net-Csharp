using GrxCAD.ApplicationServices;
using GrxCAD.DatabaseServices;
using GrxCAD.Runtime;
using GrxCAD.Geometry;

namespace CreateModelViewport
{
    public class CreateModelViewport
    {
        [CommandMethod("ty_CreateModelViewport")]
        public static void cmViewport()
        {
            // Get the current database获取当前数据库
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // Start a transaction启动事务
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Viewport table for read打开、读Viewport表 
                ViewportTable acVportTbl;
                acVportTbl = acTrans.GetObject(acCurDb.ViewportTableId,
                OpenMode.ForRead) as ViewportTable;

                // Check to see if the named view 'TEST_VIEWPORT' exists
                //检查视图‘TEST_VIEWPORT’是否存在
                if (acVportTbl.Has("TEST_VIEWPORT") == false)
                {
                    // Open the View table for write更新View表
                    acVportTbl.UpgradeOpen();

                    // Add the new viewport to the Viewport table and the transaction
                    //添加新视口到Viewport表并添加事务记录
                    ViewportTableRecord acVportTblRecLwr = new ViewportTableRecord();
                    acVportTbl.Add(acVportTblRecLwr);
                    acTrans.AddNewlyCreatedDBObject(acVportTblRecLwr, true);

                    // Name the new viewport 'TEST_VIEWPORT' and assign it to be
                    // the lower half of the drawing window
                    //新视口命名为‘TEST_VIEWPORT’并将绘图窗口的下半部分赋给它
                    acVportTblRecLwr.Name = "TEST_VIEWPORT";
                    acVportTblRecLwr.LowerLeftCorner = new Point2d(0, 0);
                    acVportTblRecLwr.UpperRightCorner = new Point2d(1, 0.5);

                    // Add the new viewport to the Viewport table and the transaction
                    //添加新视口到Viewport表并添加事务记录
                    ViewportTableRecord acVportTblRecUpr = new ViewportTableRecord();
                    acVportTbl.Add(acVportTblRecUpr);
                    acTrans.AddNewlyCreatedDBObject(acVportTblRecUpr, true);

                    // Name the new viewport 'TEST_VIEWPORT' and assign it to be
                    // the upper half of the drawing window
                    //新视口命名为‘TEST_VIEWPORT’并将绘图窗口的上半部分赋给它
                    acVportTblRecUpr.Name = "TEST_VIEWPORT";
                    acVportTblRecUpr.LowerLeftCorner = new Point2d(0, 0.5);
                    acVportTblRecUpr.UpperRightCorner = new Point2d(1, 1);

                    // To assign the new viewports as the active viewports, the 
                    // viewports named '*Active' need to be removed and recreated
                    // based on 'TEST_VIEWPORT'.
                    // 将新视口设为活动视口，需要删除名为'*Active'的视口并基于‘TEST_VIEWPORT’重建
                    // Step through each object in the symbol table
                    //遍历符号表里的每个对象
                    foreach (ObjectId acObjId in acVportTbl)
                    {
                        // Open the object for read读取对象
                        ViewportTableRecord acVportTblRec;
                        acVportTblRec = acTrans.GetObject(acObjId,
                        OpenMode.ForRead) as ViewportTableRecord;

                        // See if it is one of the active viewports, and if so erase it检查是否为活动视口，是就删除
                        if (acVportTblRec.Name == "*Active")
                        {
                            acVportTblRec.UpgradeOpen();
                            acVportTblRec.Erase();
                        }
                    }

                    // Clone the new viewports as the active viewports复制新视口为活动视口
                    foreach (ObjectId acObjId in acVportTbl)
                    {
                        // Open the object for read读取对象
                        ViewportTableRecord acVportTblRec;
                        acVportTblRec = acTrans.GetObject(acObjId,
                        OpenMode.ForRead) as ViewportTableRecord;

                        // See if it is one of the active viewports, and if so erase it. 检查是否为活动视口，是就删除
                        if (acVportTblRec.Name == "TEST_VIEWPORT")
                        {
                            ViewportTableRecord acVportTblRecClone;
                            acVportTblRecClone = acVportTblRec.Clone() as ViewportTableRecord;

                            // Add the new viewport to the Viewport table and the transaction添加新视口到Viewport表并添加事务记录
                            acVportTbl.Add(acVportTblRecClone);
                            acVportTblRecClone.Name = "*Active";
                            acTrans.AddNewlyCreatedDBObject(acVportTblRecClone, true);
                        }
                    }

                    // Update the display with the new tiled viewports arrangement
                    //用新的平铺视口排列更新显示
                    acDoc.Editor.UpdateTiledViewportsFromDatabase();

                    // Commit the changes提交更改
                    acTrans.Commit();
                }
                // Dispose of the transaction处置事务，回收内存
            }
        }
    }
}