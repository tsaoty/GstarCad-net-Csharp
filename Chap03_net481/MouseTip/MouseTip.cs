﻿using GrxCAD.ApplicationServices;
using GrxCAD.DatabaseServices;
using GrxCAD.EditorInput;
using GrxCAD.Runtime;
using gsDotNetARX;

namespace MouseTip
{
    public class MouseTip
    {
        //存储鼠标停留处的块参照的ObjectId
        static ObjectIdCollection blockIds = new ObjectIdCollection();
        static string blockName = "";//存储鼠标停留处的块名
        [CommandMethod("ty_StartMonitor")]
        public static void StartMonitor()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //添加鼠标监视事件
            ed.PointMonitor += new PointMonitorEventHandler(ed_PointMonitor);
        }
        static void ed_PointMonitor(object sender, PointMonitorEventArgs e)
        {
            string blockInfo = ""; //用于存储块参照的信息：名称和个数
            //获取命令行对象（鼠标监视事件的发起者），用于获取文档对象
            Editor ed = (Editor)sender;
            Document doc = ed.Document;
            //获取鼠标停留处的实体
            FullSubentityPath[] paths = e.Context.GetPickedEntities();
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                //如果鼠标停留处有实体
                if (paths.Length > 0)
                {
                    //获取鼠标停留处的实体
                    FullSubentityPath path = paths[0];
                    BlockReference blockRef = trans.GetObject(path.GetObjectIds()[0], OpenMode.ForRead) as BlockReference;
                    if (blockRef != null)//如果鼠标停留处为块参照
                    {
                        //获取块参照所属的块表记录并以写的方式打开
                        ObjectId blockId = blockRef.BlockTableRecord;
                        BlockTableRecord btr = trans.GetObject(blockId, OpenMode.ForRead) as BlockTableRecord;
                        //获取属于同一块表记录的所有块参照
                        ObjectIdCollection ids = btr.GetBlockReferenceIds(true, false);
                        //若鼠标停留处的块参照的块表记录与上一次的不同
                        if (blockName != btr.Name)
                        {
                            blockName = btr.Name;//重新设置块表记录名
                            blockIds.UnHighlightEntities();//取消上一次块表记录的块参照的亮显
                            blockIds.Clear(); //清空块参照ObjectId列表
                            blockIds = ids;  //设置需要亮显的块参照的ObjectId列表
                            blockIds.HighlightEntities();//亮显所有同名的块参照                            
                        }
                        blockInfo += "块名：" + btr.Name + "\n个数:" + blockIds.Count.ToString();
                    }

                }
                trans.Commit();
            }
            if (blockInfo != "")
            {
                e.AppendToolTipText(blockInfo);//在鼠标停留处显示提示信息                  
            }
        }
        [CommandMethod("ty_StopMonitor")]
        public static void StopMonitor()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //不亮显块参照
            blockIds.UnHighlightEntities();
            blockIds.Clear();
            //停止鼠标监视事件
            ed.PointMonitor -= new PointMonitorEventHandler(ed_PointMonitor);
        }
    }
}

