﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using GenericUtil;

namespace FrmBLL
{
  public  class ReleaseData
    {
      public static DataTable arrByteToDataTable(byte[] zipBuffer)
      {
          if (zipBuffer == null || zipBuffer.Length < 1)
              return null;
          byte[] buffer = UnZipClass.Decompress(zipBuffer);
          BinaryFormatter ser = new BinaryFormatter();
          DataSetSurrogate dss = ser.Deserialize(new MemoryStream(buffer)) as DataSetSurrogate;
          DataSet dataSet = dss.ConvertToDataSet();
          
          return dataSet.Tables[0];
      }

      public static DataSet arrByteToDataSet(byte[] zipBuffer)
      {
          if (zipBuffer == null || zipBuffer.Length < 1)
              return null;
          byte[] buffer = UnZipClass.Decompress(zipBuffer);
          BinaryFormatter ser = new BinaryFormatter();
          DataSetSurrogate dss = ser.Deserialize(new MemoryStream(buffer)) as DataSetSurrogate;
          DataSet dataSet = dss.ConvertToDataSet();

          return dataSet;
      }

      public static IDictionary<string, object> JsonToDictionary(string JosnStr)
      {
         return MapListConverter.JsonToDictionary(JosnStr);
      }
      public static string DictionaryToJson(IDictionary<string,object> Dic)
      {
          return MapListConverter.DictionaryToJson(Dic);
      }
      public static string ListDictionaryToJson(IList<IDictionary<string,object>> LsDic)
      {
          return MapListConverter.ListDictionaryToJson(LsDic);
      }
      public static IList<IDictionary<string, object>> JsonToListDictionary(string JosnStr)
      {
          return MapListConverter.JsonToListDictionary(JosnStr);
      }

    }

  public static class UnZipClass
  {
      public static byte[] Decompress(byte[] data)
      {
          try
          {
              MemoryStream ms = new MemoryStream(data);
              Stream zipStream = null;
              zipStream = new GZipStream(ms, CompressionMode.Decompress);
              byte[] dc_data = null;
              dc_data = ExtractBytesFromStream(zipStream, data.Length);
              return dc_data;
          }
          catch
          {
              return null;
          }
      }
      public static byte[] ExtractBytesFromStream(Stream zipStream, int dataBlock)
      {
          byte[] data = null;
          int totalBytesRead = 0;
          try
          {
              while (true)
              {
                  Array.Resize(ref data, totalBytesRead + dataBlock + 1);
                  int bytesRead = zipStream.Read(data, totalBytesRead, dataBlock);
                  if (bytesRead == 0)
                  {
                      break;
                  }
                  totalBytesRead += bytesRead;
              }
              Array.Resize(ref data, totalBytesRead);
              return data;
          }
          catch
          {
              return null;
          }
      }
  }
}
