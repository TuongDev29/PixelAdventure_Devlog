using System;
using UnityEngine;


[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class ShowConCacAttribute : PropertyAttribute
{
    // Có thể thêm các tham số nếu cần, ví dụ để cấu hình hiển thị
}