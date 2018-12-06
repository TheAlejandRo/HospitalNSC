Imports System.Data
Imports MySql.Data.MySqlClient
Imports MaterialDesignThemes.Wpf
Imports System.ComponentModel

Public Class Doctores

    Dim conexion As New MySqlConnection("host=127.0.0.1; user=root; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As DataTable
    Dim update_state As String = String.Empty

    Private Sub win_mov_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles win_mov.MouseLeftButtonDown
        DragMove()
    End Sub


    Private Sub btn_close_Selected(sender As Object, e As RoutedEventArgs) Handles btn_close.Selected
        Dim dlgclshw As New MessageClsDlg
        dlgclshw.Message.Text = "¿Quieres cerrar el programa?"
        DialogHost.Show(dlgclshw, "RootDialog")
    End Sub

    Private Sub cls_sesion_Selected(sender As Object, e As RoutedEventArgs) Handles cls_sesion.Selected
        Dim ventana As New Login
        ventana.Show()
        Me.Close()
    End Sub

    Private Sub Doctores_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Try
            conexion.Open()
            consulta = "SELECT idusuario FROM usuarios WHERE docnom='" & Dr_Title.Text & "' AND tipo_usuario='Doctor'"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            tabla = New DataTable
            adaptador.Fill(tabla)
            If tabla.Rows.Count = 1 Then
                panel_dr.Children.Clear()
                If tabla.Rows(0)(0).ToString = "6" Then
                    panel_dr.Children.Add(New Drpanel1)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "7" Then
                    panel_dr.Children.Add(New Drpanel2)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "8" Then
                    panel_dr.Children.Add(New Drpanel3)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "9" Then
                    panel_dr.Children.Add(New Drpanel4)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "10" Then
                    panel_dr.Children.Add(New Drpanel5)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "11" Then
                    panel_dr.Children.Add(New Drpanel6)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "12" Then
                    panel_dr.Children.Add(New Drpanel7)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "13" Then
                    panel_dr.Children.Add(New Drpanel8)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "14" Then
                    panel_dr.Children.Add(New Drpanel9)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "15" Then
                    panel_dr.Children.Add(New Drpanel10)
                    Activo()
                End If
            Else
                Dim dlgshw As New MessageDialog
                dlgshw.Message.Text = "Contacte con el Administrador del Sistema, Error No.x004-1"
                DialogHost.Show(dlgshw, "RootDialog")
            End If
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub Activo()
        Try
            update_state = "UPDATE usuarios SET estado='1' WHERE idusuario='" & idDr.Text & "'"
            comando = New MySqlCommand(update_state, conexion)
            comando.ExecuteNonQuery()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub Inactivo()
        Try
            conexion.Open()
            update_state = "UPDATE usuarios SET estado='0' WHERE idusuario='" & idDr.Text & "'"
            comando = New MySqlCommand(update_state, conexion)
            comando.ExecuteNonQuery()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub Doctores_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Inactivo()
    End Sub

    Private Sub btn_menu_Checked(sender As Object, e As RoutedEventArgs) Handles btn_menu.Checked
        lat_menu.SelectedIndex = -1
    End Sub
End Class
