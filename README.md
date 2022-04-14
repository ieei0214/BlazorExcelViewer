# BlazorExcelViewer

## Feature

A blazor server app to display excel file with Npoi(excel) and Ant-Design-Blazor(Tabs and Table).

## Components

- UploadFileComponent : For the excel file upload
![UploadFileComponent](images/uploader.jpg?raw=true "UploadFileComponent")

- BlazorExcelviewerComponent : To handle the excel file with NPOI and Ant-Design-Blazor
![BlazorExcelviewerComponent](images/viewer.jpg?raw=true "BlazorExcelviewerComponent")

## Usage

1. Upload a excel file.
2. Click the "Submit" button.

## PackageReference

- [Ant Design Blazor](https://github.com/ant-design-blazor/ant-design-blazor)
- [Npoi](https://github.com/nissl-lab/npoi)

## Docker image

You can run docker image by the following command and then see the demo.

```
docker run --name bev -itd -p 8081:80 ieei/blazorexcelviewer
```

http://localhost:8081/
