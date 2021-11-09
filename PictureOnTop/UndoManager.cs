using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureOnTop
{
    public enum enOperation{ undo, redo }
    class UndoManager
    {
        enOperation m_enOperation = enOperation.undo;

        /// <summary>
        /// storage for images
        /// so far it is implemented as internal memory, but can be done as file based storage
        /// </summary>
        List<System.Drawing.Bitmap> lst_bitmaps = new List<System.Drawing.Bitmap>();

        int m_GetCurrentIndex = 0;
        /// <summary>
        /// index of store image
        /// if it 0 that is initial image
        /// </summary>
        public int GetCurrentIndex {
            get 
            { 
                return m_GetCurrentIndex;
            }
        }
        public int GetTotalItemsInStorage() { return lst_bitmaps.Count; }

        public System.Drawing.Bitmap SetOperation(enOperation en)
        {
            m_enOperation = en;
            return DoAction(en);
        }

        public void Clean()
        {
            lst_bitmaps.Clear();
            m_GetCurrentIndex = 0;
        }

        public void AddNewImage(System.Drawing.Bitmap bm)
        {

            lst_bitmaps.Add(bm);
            m_GetCurrentIndex = lst_bitmaps.Count;
            Debug.WriteLine("Added image N" + m_GetCurrentIndex.ToString());
            return;

            #region NOT USED
            if (m_GetCurrentIndex == lst_bitmaps.Count - 1)
            {
                lst_bitmaps.Add(bm);
                m_GetCurrentIndex = lst_bitmaps.Count - 1;
                
            }
            else
            {
                lst_bitmaps.Insert(m_GetCurrentIndex, bm);
                m_GetCurrentIndex++;
            }
            #endregion
        }

        private System.Drawing.Bitmap DoAction(enOperation en)
        {
            System.Drawing.Bitmap bm = null;
            switch (en)
            {
                case enOperation.undo:
                    if (m_GetCurrentIndex >0)
                    {
                        if (m_GetCurrentIndex == lst_bitmaps.Count)
                            m_GetCurrentIndex -= 2;
                        else
                            m_GetCurrentIndex --;

                        bm = lst_bitmaps[m_GetCurrentIndex];
                        Debug.WriteLine("Retreived image N" + m_GetCurrentIndex.ToString());
                        
                    }
                    break;

                case enOperation.redo:
                    if (m_GetCurrentIndex < lst_bitmaps.Count - 1)
                    {
                        m_GetCurrentIndex++;
                        bm = lst_bitmaps[m_GetCurrentIndex];
                    }
                    break;
                default:
                    break;
            }
            return bm;
        }

        
    }
}
