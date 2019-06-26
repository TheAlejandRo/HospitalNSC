Imports MySql.Data.MySqlClient
Imports MaterialDesignThemes.Wpf
Imports System.Data
Imports System.ComponentModel

Public Class Login

    Dim modocerrar As Integer = 0
    Dim Secretaria As New Secretarias
    Dim administrador As New Administrador
    Dim Doctores As New Doctores
    Dim conexion As New MySqlConnection(My.Settings.Server)
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable

    Private Sub Window_mov_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles Window_mov.MouseLeftButtonDown
        DragMove()
    End Sub

    Private Sub btn_sig_Click(sender As Object, e As RoutedEventArgs) Handles btn_sig.Click
        Try
            conexion.Open()
            consulta = "SELECT tipo_usuario, docnom, idusuario, apnom FROM usuarios WHERE user='" & Txt_user.Text & "' AND pass='" & Txt_pass.Password & "'"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            tabla.Clear()
            adaptador.Fill(tabla)
            If tabla.Rows.Count = 1 Then
                If tabla.Rows(0)(0).ToString = "Administrador" Or tabla.Rows(0)(0).ToString = "administrador" Or tabla.Rows(0)(0).ToString = "Administradora" Or tabla.Rows(0)(0).ToString = "administradora" Then
                    administrador.Admin_Title.Text = tabla.Rows(0)(1).ToString
                    administrador.IDadmin.Text = tabla.Rows(0)(2).ToString
                    administrador.Show()
                    modocerrar = 1
                    conexion.Close()
                    Me.Close()
                ElseIf tabla.Rows(0)(0).ToString = "Doctor" Or tabla.Rows(0)(0).ToString = "doctor" Then
                    Doctores.Dr_Title.Text = tabla.Rows(0)(1).ToString
                    Doctores.idDr.Text = tabla.Rows(0)(2).ToString
                    Doctores.Show()
                    modocerrar = 1
                    conexion.Close()
                    Me.Close()
                ElseIf tabla.Rows(0)(0).ToString = "Secretaria" Or tabla.Rows(0)(0).ToString = "secretaria" Then
                    Secretaria.Secretaria_Nom.Text = tabla.Rows(0)(1).ToString
                    Secretaria.IDscr.Text = tabla.Rows(0)(2).ToString
                    Secretaria.Show()
                    modocerrar = 1
                    conexion.Close()
                    Me.Close()
                End If
            ElseIf tabla.Rows.Count > 1 Then
                Dim dlgshw = New MessageDialog
                dlgshw.Message.Text = "El usuario está duplicado, por favor verifique sus credenciales."
                DialogHost.Show(dlgshw, "RootDialog")
            Else
                Dim dlgshw = New MessageDialog
                dlgshw.Message.Text = "Usuario o Contraseña incorrecta"
                DialogHost.Show(dlgshw, "RootDialog")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Log.e("Error con excepción y traza", ex, New StackFrame(True))
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub CloseWindow_Click(sender As Object, e As RoutedEventArgs) Handles CloseWindow.Click
        Dim dlgclshw = New MessageClsDlg
        dlgclshw.Message.Text = "¿Quieres cerrar el programa?"
        DialogHost.Show(dlgclshw, "RootDialog")
    End Sub

    Private Sub Login_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        If modocerrar = 1 Then
            e.Cancel = False
        Else
            Dim dlgclshw = New MessageClsDlg
            dlgclshw.Message.Text = "¿Quieres cerrar el programa?"
            DialogHost.Show(dlgclshw, "RootDialog")
        End If
    End Sub

    Private Sub Login_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        modocerrar = 0
    End Sub

    Private Sub MinWindow_Click(sender As Object, e As RoutedEventArgs) Handles MinWindow.Click
        Me.WindowState = WindowState.Minimized
    End Sub
End Class
