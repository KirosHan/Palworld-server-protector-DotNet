# Palworld-server-protector-DotNet
【新】GUI版本 Palworld服务端进程守护+内存监控+优雅重启+自动存档+Rcon
（for windows）


注意：protector-electron（旧版）已弃用

欢迎反馈bug，可提交issues或者扫码进群（添加机器人后发送Palworld即可）

![Qrcode](https://raw.githubusercontent.com/KirosHan/Palworld-server-protector-DotNet/main/PNG/qrcode.jpg)

## 功能

- 服务器配置文档(.ini)可视化编辑【New】
- Webhoot通知推送(企业微信机器人)【New】


- 内存监控（自定义阈值触发）
- 进程守护（当前如果没有服务端运行就自动重启）
- 优雅重启（内存占用达到阈值后自动发送公告并关服等待重启）
- 自动备份存档
- 轮询获取在线玩家
- Rcon指令


## 注意
- 本GUI版本复刻了命令行版本（[https://github.com/KirosHan/Palworld-server-protector](https://github.com/KirosHan/Palworld-server-protector)）所有功能
- 旧GUI版（electron）由于占用内存过高已弃用并不再维护
- 本工具大部分功能基于Rcon实现，所以使用需要提前配置服务端启用Rcon和管理员密码（如果配置不生效，也请留意steamapps/common/PalServer/Pal/Saved/SaveGames/0/.../WorldOption.sav）

## 效果图
![预览1](https://raw.githubusercontent.com/KirosHan/Palworld-server-protector-DotNet/main/PNG/2.5.1_1.png)
![预览2](https://raw.githubusercontent.com/KirosHan/Palworld-server-protector-DotNet/main/PNG/2.5.1_2.png)
## 直接下载（懒人专属）
更新时间：2024-02-02

Latest版本：V2.6.4(2)

* Fixed：当获取不到玩家steamid的情况下，也会统计到在线列表中（steamid留空）

=-=-V2.6.3(1) -=-=

- 添加了webhook玩家动态(上线/下线)通知，需配置并开启Rcon以及webhook功能

* 更详尽的错误日志
* 重构了Rcon模块
* 更多采用异步方法，避免阻塞UI线程造成界面卡顿

=-=-V2.5.3(2) -=-=

- 添加了webhook的推送选择项
* 修复了最小化后无法启动窗体的bug

=-=- V2.5.2(1) -=-=

- UI调整，添加监控台
- Webhook添加钉钉机器人支持
- 优化备份存档功能逻辑
- 运行状态保存，重启时自动加载上次关闭时状态
- Webhook支持推送在线人数
- 添加任务栏通知

=-=- V2.4.1(1) -=-=

- 添加Webhook推送功能，目前仅支持企业微信群机器人，其他数据格式正在收集中，欢迎提供。

=-=- V2.3.0(1)-=-=

- 新增服务端配置文件可视化编辑（.ini）

=-=- V2.2.0(1) -=-=

- 新增配置保存功能，不用每次打开重新配置了
- 新增Rcon的kick和ban命令
- 新增错误日志输出
- 修改了参数启动的方式，现在可以自由修改

[https://github.com/KirosHan/Palworld-server-protector-DotNet/releases](https://github.com/KirosHan/Palworld-server-protector-DotNet/releases)

如无法运行，请安装.net 6.0运行环境

## Star and a Coffee

如果这个仓库对你有用，欢迎点个Star⭐︎

也可以Buy me a coffee☕︎

![BuyMeACoffee](https://raw.githubusercontent.com/KirosHan/Palworld-server-protector-DotNet/main/PNG/buymeacoffee.png)

## 编译运行
Visual studio 2022

## Rcon
本项目Rcon功能模块改编自https://github.com/KTTI465/PalWorld-RCON-GUI

## 运行逻辑

```mermaid
graph LR
    A[初始化界面] --> B[启动内存监控]
    A --> C[服务器管理]
    A --> D[玩家管理]
    B --> E[内存超阈值处理]
    C --> F[发送服务器命令]
    D --> G[获取在线玩家]
    D --> H[踢出/封禁玩家]
    E --> I[服务器重启]
    F --> J[服务器启动/停止]
    F --> K[备份游戏数据]
    G --> L[定时更新玩家列表]
    H --> M[更新玩家状态]
    I --> N[记录日志并通知]
    K --> N
    M --> N
    L --> G
    N --> O[结束]

```
## 已知问题
1.受服务端限制，rcon发送的文本中无法保留空格，已自动替换为下划线

2.受服务端限制，rcon无法发送中文

## Star History

[![Star History Chart](https://api.star-history.com/svg?repos=KirosHan/Palworld-server-protector-DotNet&type=Date)](https://star-history.com/#KirosHan/Palworld-server-protector-DotNet&Date)


