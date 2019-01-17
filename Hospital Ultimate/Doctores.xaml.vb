Imports System.Data
Imports MySql.Data.MySqlClient
Imports MaterialDesignThemes.Wpf
Imports System.ComponentModel

Public Class Doctores

    Dim modocerrar As Integer = 0
    Dim conexion As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable
    Dim update_state As String = String.Empty

    Private Sub win_mov_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles win_mov.MouseLeftButtonDown
        DragMove()
    End Sub

    Private Sub btn_close_Selected(sender As Object, e As RoutedEventArgs) Handles btn_close.Selected
        Inactivo()
        Dim dlgclshw As New MessageClsDlg
        dlgclshw.Message.Text = "¿Quieres cerrar el programa?"
        DialogHost.Show(dlgclshw, "RootDialog")
    End Sub

    Private Sub cls_sesion_Selected(sender As Object, e As RoutedEventArgs) Handles cls_sesion.Selected
        Inactivo()
        Dim ventana As New Login
        modocerrar = 1
        ventana.Show()
        Close()
    End Sub

    Private Sub Doctores_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        modocerrar = 0
        panel_dr.Children.Clear()
        If idDr.Text = "6" Then
            panel_dr.Children.Add(New Drpanel1)
        ElseIf idDr.Text = "7" Then
            panel_dr.Children.Add(New Drpanel2)
        ElseIf idDr.Text = "8" Then
            panel_dr.Children.Add(New Drpanel3)
        ElseIf idDr.Text = "9" Then
            panel_dr.Children.Add(New Drpanel4)
        ElseIf idDr.Text = "10" Then
            panel_dr.Children.Add(New Drpanel5)
        ElseIf idDr.Text = "11" Then
            panel_dr.Children.Add(New Drpanel6)
        ElseIf idDr.Text = "12" Then
            panel_dr.Children.Add(New Drpanel7)
        ElseIf idDr.Text = "13" Then
            panel_dr.Children.Add(New Drpanel8)
        ElseIf idDr.Text = "14" Then
            panel_dr.Children.Add(New Drpanel9)
        ElseIf idDr.Text = "15" Then
            panel_dr.Children.Add(New Drpanel10)
        End If
    End Sub

    Public Sub Activo()
        Try
            update_state = "UPDATE usuarios SET estado='1' WHERE idusuario='" & idDr.Text & "'"
            comando = New MySqlCommand(update_state, conexion)
            comando.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Log.e("Error con excepción y Traza", ex, New StackFrame(True))
        End Try
    End Sub

    Public Sub Inactivo()
        Try
            conexion.Open()
            update_state = "UPDATE usuarios SET estado='0' WHERE idusuario='" & idDr.Text & "'"
            comando = New MySqlCommand(update_state, conexion)
            comando.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Log.e("Error con excepción y Traza", ex, New StackFrame(True))
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub btn_menu_Checked(sender As Object, e As RoutedEventArgs) Handles btn_menu.Checked
        lat_menu.SelectedIndex = -1
    End Sub

    Private Sub Doctores_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        If modocerrar = 1 Then
            e.Cancel = False
        Else
            Inactivo()
            Dim dlgclshw As New MessageClsDlg
            dlgclshw.Message.Text = "¿Quieres cerrar el programa?"
            DialogHost.Show(dlgclshw, "RootDialog")
        End If
    End Sub
End Class
