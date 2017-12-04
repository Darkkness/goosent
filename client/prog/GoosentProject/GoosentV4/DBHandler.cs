using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database.Sqlite;

namespace Goosent
{
    class DBHandler : SQLiteOpenHelper
    {
        public DBHandler(Context context) : base(context, "GoosentDB", null, 1)
        {
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL("create table GoosentDB (id integer primary key autoincrement, name text, email text)");
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            throw new NotImplementedException();
        }
    }
}