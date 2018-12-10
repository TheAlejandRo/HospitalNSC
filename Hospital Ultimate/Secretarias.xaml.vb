Imports System.Data
Imports MySql.Data.MySqlClient
Imports MaterialDesignThemes.Wpf
Imports System.ComponentModel

Public Class Secretarias

    Dim conexion As New MySqlConnection("host=127.0.0.1; user=root; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable
    Dim update_state As String = String.Empty

    Private Sub win_move_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles win_move.MouseLeftButtonDown
        DragMove()
    End Sub

    Private Sub btn_menu_Click(sender As Object, e As RoutedEventArgs) Handles btn_menu.Click
        lat_menu.SelectedIndex = -1
    End Sub

    Private Sub config_pnlespera_Selected(sender As Object, e As RoutedEventArgs) Handles config_pnlespera.Selected
        btn_menu.IsChecked = False
        DoctoresActivos.IsEnabled = False
        DoctoresActivos.Visibility = Visibility.Collapsed
        FuncionesExtras.IsEnabled = True
        FuncionesExtras.Visibility = Visibility.Visible
    End Sub

    Private Sub btn_close_Selected(sender As Object, e As RoutedEventArgs) Handles btn_close.Selected
        btn_menu.IsChecked = False
        Dim dlgclshw As New MessageClsDlg
        dlgclshw.Message.Text = "¿Quieres cerrar el programa?"
        DialogHost.Show(dlgclshw, "RootDialog")
    End Sub

    Private Sub cls_sesion_Selected(sender As Object, e As RoutedEventArgs) Handles cls_sesion.Selected
        btn_menu.IsChecked = False
        Dim login As New Login
        login.Show()
        Me.Close()
    End Sub

    Private Sub pnl_espera_Selected(sender As Object, e As RoutedEventArgs) Handles pnl_espera.Selected
        btn_menu.IsChecked = False
        Dim panel As New MainWindow
        panel.Show()
    End Sub

    Private Sub Secretarias_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub

    Private Sub Activo()
        Try
            update_state = "UPDATE usuarios SET estado='1' WHERE idusuario='" & IDscr.Text & "'"
            comando = New MySqlCommand(update_state, conexion)
            comando.ExecuteNonQuery()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Secretarias_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            conexion.Open()
            update_state = "UPDATE usuarios SET estado='0' WHERE idusuario='" & IDscr.Text & "'"
            comando = New MySqlCommand(update_state, conexion)
            comando.ExecuteNonQuery()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub generador_ticket_Selected(sender As Object, e As RoutedEventArgs) Handles generador_ticket.Selected
        btn_menu.IsChecked = False
        DoctoresActivos.IsEnabled = True
        DoctoresActivos.Visibility = Visibility.Visible
        FuncionesExtras.IsEnabled = False
        FuncionesExtras.Visibility = Visibility.Collapsed
    End Sub
End Class