
Imports System.Runtime.InteropServices
Imports System.ComponentModel

Public Class clsLongFIle

    Structure FILETIME
        Public dwLowDateTime As Long
        Public dwHighDateTime As Long
    End Structure

    Structure WIN32_FIND_DATA
        Public dwFileAttributes As System.IO.FileAttributes
        Public ftCreationTime As FILETIME
        Public ftLastAccessTime As FILETIME
        Public ftLastWriteTime As FILETIME
        Public nFileSizeHigh As Long
        Public nFileSizeLow As Long
        Public dwReserved0 As Long
        Public dwReserved1 As Long
        Public cFileName As String
        Public cAlternate As String
    End Structure

    Private Const DRIVE_NO_ROOT_DIR As Long = 1
    Private Const ERROR_SHARING_VIOLATION As Long = 32
    Private Const MAX_PATH As Long = 260

    Private Const FILE_ATTRIBUTE_ARCHIVE = &H20
    Private Const INVALID_FILE_ATTRIBUTES = -1

    Private Const FILE_READ_DATA = &H1
    Private Const FILE_WRITE_DATA = &H2
    Private Const FILE_APPEND_DATA = &H4
    Private Const FILE_READ_EA = &H8
    Private Const FILE_WRITE_EA = &H10

    Private Const FILE_READ_ATTRIBUTES = &H80
    Private Const FILE_WRITE_ATTRIBUTES = &H100

    Private Const FILE_SHARE_NONE = &H0
    Private Const FILE_SHARE_READ = &H1

    Private Const FILE_ATTRIBUTE_DIRECTORY = &H10

    Private Const FILE_GENERIC_WRITE = STANDARD_RIGHTS_WRITE Or FILE_WRITE_DATA Or
                                                FILE_WRITE_ATTRIBUTES Or
                                                FILE_WRITE_EA Or
                                                FILE_APPEND_DATA Or
                                                SYNCHRONIZE

    Private Const FILE_GENERIC_READ = STANDARD_RIGHTS_READ Or
                                            FILE_READ_DATA Or
                                            FILE_READ_ATTRIBUTES Or
                                            FILE_READ_EA Or
                                            SYNCHRONIZE



    Private Const READ_CONTROL = &H20000L
    Private Const STANDARD_RIGHTS_READ = READ_CONTROL
    Private Const STANDARD_RIGHTS_WRITE = READ_CONTROL

    Private Const SYNCHRONIZE = &H100000L

    Private Const CREATE_NEW = 1
    Private Const CREATE_ALWAYS = 2
    Private Const OPEN_EXISTING = 3

    Private Const MAX_ALTERNATE = 14

    Private Declare Function FindClose Lib "kernel32" (ByVal hFindFile As Long) As Long
    Private Declare Function FindFirstFileW Lib "kernel32" (ByVal lpFileName As Long, ByRef lpFindFileData As WIN32_FIND_DATA) As Long
    Private Declare Function GetDriveTypeW Lib "kernel32" (ByVal lpRootPathName As Long) As Long
    Private Declare Function GetFileAttributesW Lib "kernel32" (ByVal lpFileName As Long) As Long
    Private Declare Function PathFileExistsW Lib "shlwapi" (ByVal pszPath As Long) As Long
    Private Declare Function PathIsDirectoryW Lib "shlwapi" (ByVal pszPath As Long) As Long
    Private Declare Function CopyFileW Lib "kernel32" (lpExistingFileName As String, lpNewFileName As String, bFailIfExists As Boolean) As Boolean
    Private Declare Function DeleteFileW Lib "kernel32" (lpFileName As String) As Boolean
    Private Declare Function GetFileTime Lib "kernel32" (hFile As Microsoft.Win32.SafeHandles.SafeFileHandle, ByRef lpCreationTime As Date, ByRef lpLastAccessTime As Date, ByRef lpLastWriteTime As Date) As Boolean

    Private Declare Function CreateFileW Lib "kernel32" (lpFileName As String, dwDesiredAccess As Int32, dwShareMode As Int32, lpSecurityAttributes As IntPtr, dwCreationDisposition As Int32, dwFlagsAndAttributes As Int32, hTemplateFile As IntPtr) As Microsoft.Win32.SafeHandles.SafeFileHandle

    Private Declare Function CreateDirectoryW Lib "kernel32" (lpPathName As String, lpSecurityAttributes As Long) As Boolean

    Private Function GetFileHandle(filename As String) As Microsoft.Win32.SafeHandles.SafeFileHandle
        Dim hHandle As Microsoft.Win32.SafeHandles.SafeFileHandle

        'Dim hHandle As IntPtr

        hHandle = CreateFileW(filename, FILE_GENERIC_READ, FILE_SHARE_READ, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero)
        If hHandle.IsInvalid Then
            Dim code As Integer = Marshal.GetLastWin32Error()

            ' other errors, throw exception
            Throw New Win32Exception(code)
            Err.Raise(Err.Number)
        Else
            GetFileHandle = hHandle
        End If
    End Function

    Public Sub GetFileInfo(strFileName As String, ByRef dtmCreationTime As Date, ByRef dtmAccessTime As Date, ByRef dtmModifiedTime As Date)
        Dim hHandle As Microsoft.Win32.SafeHandles.SafeFileHandle

        hHandle = GetFileHandle(strFileName)
        GetFileTime(hHandle, dtmCreationTime, dtmAccessTime, dtmModifiedTime)

    End Sub

End Class
