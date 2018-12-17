Imports MaterialDesignThemes.Wpf
Imports MySql.Data.MySqlClient
Imports System.Data

Public Class Administrador

    Dim row As DataRowView
    Dim conexion As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable

    Private Sub win_move_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles win_move.MouseLeftButtonDown
        DragMove()
    End Sub

    Private Sub btn_close_Click(sender As Object, e As RoutedEventArgs) Handles btn_close.Selected
        btn_menu.IsChecked = False
        Dim dlgclshw = New MessageClsDlg
        dlgclshw.Message.Text = "¿Quieres cerrar el programa?"
        DialogHost.Show(dlgclshw, "RootDialog")
    End Sub

    Private Sub btn_menu_Click(sender As Object, e As RoutedEventArgs) Handles btn_menu.Click
        lat_menu.SelectedIndex = -1
    End Sub

    Private Sub cls_sesion_Selected(sender As Object, e As RoutedEventArgs) Handles cls_sesion.Selected
        Dim Login As New Login
        Login.Show()
        Me.Finalize()
        Me.Close()
    End Sub

    Private Sub Administrador_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Lista()
    End Sub

    Private Sub UserDR_Selected(sender As Object, e As RoutedEventArgs) Handles UserDR.Selected
        btn_menu.IsChecked = False
        Usuariosgeneral.IsEnabled = False
        Usuariosgeneral.Visibility = Visibility.Collapsed
        PgAdmin.IsEnabled = True
        PgAdmin.Visibility = Visibility.Visible
        PgAdmin.Children.Clear()
        PgAdmin.Children.Add(New UsersDoc)
    End Sub

    Private Sub UserAD_Selected(sender As Object, e As RoutedEventArgs) Handles UserAD.Selected
        btn_menu.IsChecked = False
        Usuariosgeneral.IsEnabled = False
        Usuariosgeneral.Visibility = Visibility.Collapsed
        PgAdmin.IsEnabled = True
        PgAdmin.Visibility = Visibility.Visible
        PgAdmin.Children.Clear()
        PgAdmin.Children.Add(New UsersAD)
    End Sub

    Private Sub UserSCR_Selected(sender As Object, e As RoutedEventArgs) Handles UserSCR.Selected
        btn_menu.IsChecked = False
        Usuariosgeneral.IsEnabled = False
        Usuariosgeneral.Visibility = Visibility.Collapsed
        PgAdmin.IsEnabled = True
        PgAdmin.Visibility = Visibility.Visible
        PgAdmin.Children.Clear()
        PgAdmin.Children.Add(New UsersScr)
    End Sub

    Private Sub Usersgnl_Selected(sender As Object, e As RoutedEventArgs) Handles Usersgnl.Selected
        btn_menu.IsChecked = False
        Usuariosgeneral.IsEnabled = True
        Usuariosgeneral.Visibility = Visibility.Visible
        PgAdmin.IsEnabled = False
        PgAdmin.Visibility = Visibility.Collapsed
    End Sub

    Private Sub Btnactualizar_Click(sender As Object, e As RoutedEventArgs) Handles Btnactualizar.Click
        CodClinica.Text = ""
        Nameuser.Text = ""
        Firstuser.Text = ""
        user.Text = ""
        Txt_newpassword.Text = ""
        IDnew.Text = ""
        tel1.Text = ""
        tel2.Text = ""
        tel3.Text = ""
        TypeUser.Text = ""
        Usersgrid.SelectedItems.Clear()
        Usersgrid.SelectedIndex = -1
        Lista()
    End Sub

    Private Sub Lista()
        Try
            conexion.Open()
            consulta = "SELECT * FROM usuarios"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            tabla.Clear()
            adaptador.Fill(tabla)
            Usersgrid.ItemsSource = tabla.DefaultView
        Catch ex As MySqlException
            MsgBox(ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub Usersgrid_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles Usersgrid.SelectedCellsChanged
        If Usersgrid.SelectedIndex <> -1 Then
            row = Usersgrid.SelectedItem
            CodClinica.Text = row.Row.ItemArray(8).ToString
            Nameuser.Text = row.Row.ItemArray(3).ToString
            Firstuser.Text = row.Row.ItemArray(4).ToString
            user.Text = row.Row.ItemArray(2).ToString
            Txt_newpassword.Text = row.Row.ItemArray(10).ToString
            IDnew.Text = row.Row.ItemArray(0).ToString
            tel1.Text = row.Row.ItemArray(5).ToString
            tel2.Text = row.Row.ItemArray(6).ToString
            tel3.Text = row.Row.ItemArray(7).ToString
            TypeUser.Text = row.Row.ItemArray(1).ToString
        Else
            CodClinica.Text = ""
            Nameuser.Text = ""
            Firstuser.Text = ""
            user.Text = ""
            Txt_newpassword.Text = ""
            IDnew.Text = ""
            tel1.Text = ""
            tel2.Text = ""
            tel3.Text = ""
            TypeUser.Text = ""
            row.CancelEdit()
        End If
    End Sub
End Class