# 🔨 Build & Run Instructions

## Cách 1: Build với Visual Studio (Khuyến nghị)

### Bước 1: Mở Solution
```
Double-click vào: XTStyle.sln
```

### Bước 2: Build
```
Menu: Build > Rebuild Solution
Hoặc: Ctrl + Shift + B
```

### Bước 3: Set StartUp Project
```
Right-click vào "XTStyle.Demo" project
Chọn: "Set as StartUp Project"
```

### Bước 4: Run
```
Nhấn F5 hoặc click nút "Start"
```

---

## Cách 2: Build từ Command Line

### Tìm MSBuild
```powershell
$msbuild = & "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe | Select-Object -First 1
```

### Build Debug
```powershell
& $msbuild XTStyle.sln /t:Rebuild /p:Configuration=Debug
```

### Build Release
```powershell
& $msbuild XTStyle.sln /t:Rebuild /p:Configuration=Release
```

### Run Demo
```powershell
.\XTStyle.Demo\bin\Debug\XTStyle.Demo.exe
```

---

## 🎯 Kiểm tra kết quả

Sau khi run, bạn sẽ thấy:

✅ **Header** với title và theme switcher
✅ **Breadcrumb** navigation
✅ **Search & Filter** section với SearchBox, ComboBox, DateRangePicker
✅ **4 Stats Cards** hiển thị metrics
✅ **3 Tabs** (Products, Form Controls, Advanced)
✅ **Products Tab**: DataGrid với 15 products + Pagination
✅ **Form Controls Tab**: Tất cả form inputs
✅ **Advanced Tab**: ProgressTracker + Accordion
✅ **Toast Notifications** xuất hiện khi load
✅ **Modal Dialog** khi click "Show Modal"
✅ **Loading Spinner** khi click "Refresh"

---

## ⚠️ Troubleshooting

### Lỗi: "Could not load file or assembly 'XTStyle'"
**Giải pháp**: Build lại XTStyle project trước
```
Right-click XTStyle project > Build
```

### Lỗi: "The type or namespace name 'XTStyle' could not be found"
**Giải pháp**: Kiểm tra project reference
```
Right-click XTStyle.Demo > Add > Reference > Projects > Check XTStyle
```

### Lỗi: Build failed
**Giải pháp**: Clean và rebuild
```
Build > Clean Solution
Build > Rebuild Solution
```

---

## 📝 Notes

- **Target Framework**: .NET Framework 4.8
- **Build Time**: ~30-60 seconds (lần đầu)
- **Output**: `XTStyle.Demo\bin\Debug\XTStyle.Demo.exe`
- **Dependencies**: Chỉ cần .NET Framework 4.8, không cần package nào khác

---

**Happy Testing! 🎉**
