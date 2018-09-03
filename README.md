# 项目介绍
此项目为[ABManager](https://github.com/qbhhc/ABManager)相对应的web服务器工程，目前用于客户端的更新过程

##### 项目环境
    vs2017+asp.net core 2.1
    
##### 发布环境
    IIS+FTP

##### 文件夹介绍
    1 FengHC\LocalResources\AssetBundles\ 文件夹用来存放客户端build出的ab资源以及cache.txt ，dep.all 文件
    2 FengHC\Controllers\ 文件夹是服务器路由接口代码
    3 FengHC\UnitySystem\ 文件夹是处理unity客户端逻辑的一些代码

##### 使用步骤
    1 打开工程，点击项目名运行，确定项目环境没有问题
    2 在本地IIS服务器（或者服务器上的IIS服务器）上建立网站，并添加FTP站点
    3 右击工程名，点击发布，选择IIS,FTP发布-》发布方法选择FTP，按要求填好后点击发布，将网站发布到服务器
    4 发布成功后，重启IIS服务器上的网站
    5 点击网站右边的浏览，能进入asp home page网页表示web服务器搭建成功
 
