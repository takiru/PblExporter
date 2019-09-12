using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PblExporter
{
    /// <summary>
    /// ListViewの項目の並び替えに使用するクラス
    /// </summary>
    public class ListViewItemComparer : IComparer
    {
        private int _column;
        private bool _asc;

        /// <summary>
        /// ListViewItemComparerクラスのコンストラクタ
        /// </summary>
        /// <param name="col">並び替える列番号</param>
        /// <param name="asc">昇順かどうか</param>
        public ListViewItemComparer(int col, bool asc)
        {
            _column = col;
            _asc = asc;
        }

        //xがyより小さいときはマイナスの数、大きいときはプラスの数、
        //同じときは0を返す
        public int Compare(object x, object y)
        {
            //ListViewItemの取得
            ListViewItem itemx = (ListViewItem)x;
            ListViewItem itemy = (ListViewItem)y;

            //xとyを文字列として比較する
            if (_asc)
            {
                return string.Compare(itemx.SubItems[_column].Text, itemy.SubItems[_column].Text);
            } else
            {
                return string.Compare(itemy.SubItems[_column].Text, itemx.SubItems[_column].Text);
            }
        }
    }
}
