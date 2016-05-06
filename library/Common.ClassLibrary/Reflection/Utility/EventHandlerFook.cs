using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Common.ClassLibrary.Refrection.Utility
{
    public class EventHandlerFook
    {
        /// <summary>
        /// 対象オブジェクトのイベントハンドラを取得する
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public static Delegate GetEventHandler(object obj, string eventName)
        {
            EventHandlerList ehl = GetEvents(obj);
            object key = GetEventKey(obj, eventName);
            return ehl[key];
        }

        private delegate MethodInfo delGetEventsMethod(Type objType, delGetEventsMethod callback);

        private static EventHandlerList GetEvents(object obj)
        {
            delGetEventsMethod GetEventsMethod = delegate(Type objtype, delGetEventsMethod callback)
            {
                MethodInfo mi = objtype.GetMethod("get_Events", All);
                if ((mi == null) & (objtype.BaseType != null))
                    mi = callback(objtype.BaseType, callback);
                return mi;
            };

            MethodInfo methodInfo = GetEventsMethod(obj.GetType(), GetEventsMethod);
            if (methodInfo == null) return null;
            return (EventHandlerList)methodInfo.Invoke(obj, new object[] { });
        }

        private delegate FieldInfo delGetKeyField(Type objType, string eventName, delGetKeyField callback);
        private static object GetEventKey(object obj, string eventName)
        {
            delGetKeyField GetKeyField = delegate(Type objtype, string eventname, delGetKeyField callback)
            {
                FieldInfo fi = objtype.GetField("Event" + eventName, All);
                if ((fi == null) & (objtype.BaseType != null))
                    fi = callback(objtype.BaseType, eventName, callback);
                return fi;
            };

            FieldInfo fieldInfo = GetKeyField(obj.GetType(), eventName, GetKeyField);
            if (fieldInfo == null) return null;
            return fieldInfo.GetValue(obj);
        }

        private static BindingFlags All
        {
            get
            {
                return
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance |
                    BindingFlags.IgnoreCase |
                    BindingFlags.Static;
            }
        }

        /// <summary>
        /// バインドされているイベントを全て解除する。
        /// </summary>
        /// <param name="component">イベントハンドラを削除するコンポーネント</param>
        public static void ClearEvent(Component component)
        {

            if (component == null) { return; }

            // イベントハンドラのリストを取得する
            BindingFlags flag = BindingFlags.NonPublic | BindingFlags.Instance;
            EventHandlerList evList = typeof(Component).GetField("events", flag).GetValue(component) as EventHandlerList;

            if (evList == null) { return; }

            // イベントハンドラのKey情報を含むHead情報を取得する
            object evEntryData = evList.GetType().GetField("head", flag).GetValue(evList);

            if (evEntryData == null) { return; }

            // バインドされているイベントハンドラが無くなるまで解除処理をループ
            do
            {
                object key = evEntryData.GetType().GetField("key", flag).GetValue(evEntryData);

                if (key == null) { break; }

                evList[key] = null;
                evEntryData = evEntryData.GetType().GetField("next", flag).GetValue(evEntryData);

            }
            while (evEntryData != null);
        }
    }
}
