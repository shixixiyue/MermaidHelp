# 使用官方的 .NET SDK 镜像作为构建环境
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# 设置工作目录
WORKDIR /app

# 复制 csproj 并还原依赖项（利用缓存）
COPY MermaidHelp-Web/*.csproj ./MermaidHelp-Web/
RUN dotnet restore ./MermaidHelp-Web/MermaidHelp.csproj

# 复制所有文件并构建发布版本
COPY . .
RUN dotnet publish ./MermaidHelp-Web/MermaidHelp.csproj -c Release -o /app/out

# 使用官方的 .NET 运行时镜像作为运行环境
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# 设置工作目录
WORKDIR /app

# 从构建环境中复制发布版本的输出
COPY --from=build-env /app/out .

# 设置环境变量
ENV MODEL=gpt-4o
ENV URL=https://api.chatanywhere.tech/v1/chat/completions
ENV KEY=

# 暴露端口
EXPOSE 80

# 启动应用
ENTRYPOINT ["dotnet", "MermaidHelp.dll"]
