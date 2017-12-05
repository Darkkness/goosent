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
        string SETS_TABLE_NAME;
        string CHANNELS_TABLE_NAME;
        const string KEY_CHANNEL_ID = "id";
        const string KEY_CHANNEL_NAME = "channel_name";
        const string KEY_CHANNEL_PLATFORM = "platform";
        const string KEY_CHANNEL_SET_ID = "channel_set_id";
        const string KEY_SET_ID = "set_id";
        const string KEY_SET_NAME = "set_name";
        
        Context _context;
        public DBHandler(Context context) : base(context, context.Resources.GetString(Resource.String.database_name), null, 1)
        {
            _context = context;
            DATABASE_NAME = _context.Resources.GetString(Resource.String.database_name);
            SETS_TABLE_NAME = _context.Resources.GetString(Resource.String.database_sets_table_name);
            CHANNELS_TABLE_NAME = _context.Resources.GetString(Resource.String.database_channels_table_name);
        }


        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL("CREATE TABLE " + SETS_TABLE_NAME +
                " (" + KEY_SET_ID + " integer primary key autoincrement, "
                 + KEY_SET_NAME + " text)");

            db.ExecSQL("CREATE TABLE " + CHANNELS_TABLE_NAME +
                " (" + KEY_CHANNEL_ID + " integer primary key autoincrement, "
                 + KEY_CHANNEL_NAME + " text, "
                 + KEY_CHANNEL_PLATFORM + " text, "
                 + KEY_CHANNEL_SET_ID + " integer)");
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXISTS " + DATABASE_NAME);


            OnCreate(db);
        }

        /// <summary>
        /// Add channel to the local DB
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="set_id"></param>
        public void AddChannel(Channel channel, int set_id)
        {
            SQLiteDatabase db = WritableDatabase;
            ContentValues values = new ContentValues();
            values.Put(KEY_CHANNEL_NAME, channel.Name);
            values.Put(KEY_CHANNEL_PLATFORM, channel.Platform);
            values.Put(KEY_CHANNEL_SET_ID, set_id);

            var id = db.Insert(CHANNELS_TABLE_NAME, null, values);
            db.Close();
        }

        /// <summary>
        /// Add set and all it's channels to the local DB
        /// </summary>
        /// <param name="set"></param>
        public void AddSet(ChannelsSet set)
        {
            SQLiteDatabase db = WritableDatabase;
            ContentValues sets_table_values = new ContentValues();

            sets_table_values.Put(KEY_SET_NAME, set.Name);
            var id = db.Insert(SETS_TABLE_NAME, null, sets_table_values);

            foreach (Channel channel in set.Channels)
            {
                AddChannel(channel, (int)id);
            }

            db.Close();
        }

        public Channel GetChannel(int id)
        {
            SQLiteDatabase db = ReadableDatabase;
            ICursor cursor = db.Query(CHANNELS_TABLE_NAME, new string[] { KEY_CHANNEL_ID, KEY_CHANNEL_NAME, KEY_CHANNEL_PLATFORM, KEY_CHANNEL_SET_ID }, KEY_CHANNEL_ID + "=?", new string[] { id.ToString() }, null, null, null, null);
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

        public void ClearDatabase()
        {
            SQLiteDatabase db = WritableDatabase;
            try
            {
                db.Delete(SETS_TABLE_NAME, null, null);
            } catch (Exception) {

            };
            try
            {
                db.Delete(CHANNELS_TABLE_NAME, null, null);
            }
            catch (Exception)
            {

            };
            db.ExecSQL("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='" + CHANNELS_TABLE_NAME + "'");
            db.ExecSQL("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='" + SETS_TABLE_NAME + "'");
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

        //public void DeleteDatabase()
        //{
        //    try
        //    {
        //        _context.DeleteDatabase(DATABASE_NAME);
        //    } catch (Exception)
        //    {
        //        Console.WriteLine("Unable to delete database");
        //    }
            
        //}
    }
}