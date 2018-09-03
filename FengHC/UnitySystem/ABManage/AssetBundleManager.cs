using System;
using FengHC.Log;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.FileProviders;
using FengHC.UnitySystem.Common;

namespace FengHC.UnitySystem.ABManage
{
    public class AssetBundleManager
    {

        #region 公有数据
        /// <summary>
        /// 本地ab文件夹名
        /// </summary>
        public static string assetbundleFullName = "LocalResources/AssetBundles/";
        #endregion

        #region 公有方法
        /// <summary>
        /// 初始化方法
        /// </summary>
        public static void Init()
        {

        }

        /// <summary>
        /// 获取ab 的dep.all 的文本内容
        /// </summary>
        public static string GetABDepFileText(IFileProvider fileProvider)
        {
            try
            {
                return FileManager.ReadTextFile(fileProvider, assetbundleFullName + "dep.all");
            }
            catch (Exception e)
            {
                return "Error:" + e.Message;
            }

        }

        /// <summary>
        /// 获取ab cache.txt的文本内容
        /// </summary>
        public static string GetABCacheFileText(IFileProvider fileProvider)
        {
            try
            {
                return FileManager.ReadTextFile(fileProvider, assetbundleFullName + "cache.txt");
            }
            catch (Exception e)
            {
                return "Error:" + e.Message;
            }
        }

        /// <summary>
        ///  获取ab文件 参数1 本地文件操作类（封装了system.io）参数2：获取ab的下载model名字
        ///  返回 ab文件model的json串
        /// </summary>
        public static byte[] GetABFile(IFileProvider fileProvider, string getABmodelName)
        {
            //string Json字符串 = JsonConvert.SerializeObject(目标对象);
            //Product deserializedProduct = (Product)JsonConvert.DeserializeObject(output, typeof(Product));
            try
            {
                if (getABmodelName != null)
                {
                    //ab资源
                    var datas = FileManager.ReadFile(fileProvider, assetbundleFullName + getABmodelName);
                    return datas;
                    //return JsonConvert.SerializeObject(abModel);
                }

            }
            catch (Exception e)
            {
                DebugManager.LogError(getABmodelName + " GetABFile Error:" + e.Message);
            }

            return null;
        }



        /// <summary>
        ///  获取ab文件 参数1 本地文件操作类（封装了system.io）参数2：获取ab的下载model
        ///  返回 ab文件model的json串
        /// </summary>
        public static AssetBundleDownloadModel GetABFile2(IFileProvider fileProvider, string getABmodel)
        {
            //string Json字符串 = JsonConvert.SerializeObject(目标对象);
            //Product deserializedProduct = (Product)JsonConvert.DeserializeObject(output, typeof(Product));
            try
            {
                AssetBundleDownloadModel abModel = JsonConvert.DeserializeObject(getABmodel, typeof(AssetBundleDownloadModel)) as AssetBundleDownloadModel;
                if (abModel != null)
                {
                    //ab资源
                    var datas = FileManager.ReadFile(fileProvider, assetbundleFullName + abModel.assetBundleFileName);
                    abModel.assetBundleData = datas;
                    abModel.assetBundleDataString = System.Text.Encoding.UTF8.GetString(datas);

                    //mianfest资源
                    datas = FileManager.ReadFile(fileProvider, assetbundleFullName + abModel.assetBundleManifestFileName);
                    abModel.assetBundleManifestData = datas;
                    abModel.assetBundleManifestDataString = System.Text.Encoding.UTF8.GetString(datas);

                    return abModel;
                    //return JsonConvert.SerializeObject(abModel);
                }

            }
            catch (Exception e)
            {
                DebugManager.LogError("GetABFile Error:" + e.Message);
            }

            return null;
        }

        #endregion

    }

    #region abmodel
    /// <summary>
    /// 下载的ab资源model
    /// </summary>
    [System.Serializable]
    public class AssetBundleDownloadModel
    {
        /// <summary>
        /// 资源名字（资源的唯一标志）(目前名字是该资源在Asset下的路径名字  如Assets\Prefabs\Capsule.prefab)
        /// </summary>
        public string assetBundleName;

        /// <summary>
        /// 资源文件的名字（3a85b78f45e66d3862c78b03de9e64e228bedc8c.ab）
        /// </summary>
        public string assetBundleFileName;

        /// <summary>
        /// 资源依赖文件的名字（3a85b78f45e66d3862c78b03de9e64e228bedc8c.ab.manifest）
        /// </summary>
        public string assetBundleManifestFileName;


        /// <summary>
        /// 资源数据的字符串
        /// </summary>
        public string assetBundleDataString;

        /// <summary>
        /// 资源依赖文件数据的字符串
        /// </summary>
        public string assetBundleManifestDataString;

        /// <summary>
        /// 资源数据
        /// </summary>
        public byte[] assetBundleData;

        /// <summary>
        /// 资源依赖文件
        /// </summary>
        public byte[] assetBundleManifestData;

    }
    #endregion
}
