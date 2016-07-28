﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;

namespace DropDownList.DBOperator
{
    //用于将DataTable转换成List数组
    public static class DatatableToList<T> where T:new()
    {
        public static IList<T> ConvertToModel(DataTable dt)
        {
            //定义集合
            IList<T> ts = new List<T>();
            T t = new T();
            string tempName = "";
            //获取此模型的公共属性
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (DataRow row in dt.Rows)
            {
                t = new T();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite)
                        {
                            continue;    
                        }
                        object value = row[tempName];
                        if (value!=DBNull.Value)
                        {
                            pi.SetValue(t, value, null);
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}