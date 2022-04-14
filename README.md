# BlazorExcelViewer

## Feature

A blazor server app to display excel file with Ant-Design-Blazor "Tabs" and "Table".

## Components

- UploadFileComponent : For excel file upload
- BlazorExcelviewerComponent : To handle the excel file with NPOI and Ant-Design-Blazor

## PackageReference

- Ant Design Blazor https://github.com/ant-design-blazor/ant-design-blazor
- NPOI https://github.com/nissl-lab/npoi

## Docker image

You can run docker image by the following command and then see the demo.

docker run --name bev -itd -p 8081:80 ieei/blazorexcelviewer

http://localhost:8081/
