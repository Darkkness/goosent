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
using Android.Database;

namespace Goosent
{
    class DBHandler : SQLiteOpenHelper
    {
        string DATABASE_NAME;
        string TABLE_NAME;
        const string KEY_ID = "id";
        const string KEY_CHANNEL_NAME = "channel_name";
        const string KEY_CHANNEL_PLATFORM = "platform";

        Context _context;
        public DBHandler(Context context) : base(context, context.Resources.GetString(Resource.String.database_name), null, 1)
        {
            _context = context;
            DATABASE_NAME = _context.Resources.GetString(Resource.String.database_name);
            TABLE_NAME = _context.Resources.GetString(Resource.String.database_table_name);
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL("CREATE TABLE " + TABLE_NAME +
                " (" + KEY_ID + " integer primary key autoincrement, "
                 + KEY_CHANNEL_NAME + " text, "
                 + KEY_CHANNEL_PLATFORM + " text)");
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXISTS " + Resource.String.database_name);


            OnCreate(db);
        }

        public void AddChannel(Channel channel)
        {
            SQLiteDatabase db = WritableDatabase;
            ContentValues values = new ContentValues();
            values.Put(KEY_CHANNEL_NAME, channel.Name);
            values.Put(KEY_CHANNEL_PLATFORM, channel.Platform);

            db.Insert(TABLE_NAME, null, values);
            db.Close();
        }

        public Channel GetChannel(int id)
        {
            SQLiteDatabase db = ReadableDatabase;
            ICursor cursor = db.Query(TABLE_NAME, new string[] { KEY_ID, KEY_CHANNEL_NAME, KEY_CHANNEL_PLATFORM }, KEY_ID + "=?", new string[] { id.ToString() }, null, null, null, null);
            if (cursor != null)
            {
                cursor.MoveToFirst();
            }

            // Канал найден
            if (cursor.Count > 0)
            {
                Channel channel = new Channel(cursor.GetString(1), cursor.GetString(2));

                return channel;
            }


            return null;
        }

        public String GetTableAsString(String tableName)
        {
            SQLiteDatabase db = ReadableDatabase;
            String tableString = string.Format("Table {0}\n", tableName);
            ICursor allRows = db.RawQuery("SELECT * FROM " + tableName, null);
            if (allRows.MoveToFirst())
            {
                String[] columnNames = allRows.GetColumnNames();
                do
                {
                    foreach (String name in columnNames)
                    {
                        tableString += string.Format("{0}: {1}\n", name,
                                allRows.GetString(allRows.GetColumnIndex(name)));
                    }
                    tableString += "\n";

                } while (allRows.MoveToNext());
            }

            return tableString;
        }
    }
}