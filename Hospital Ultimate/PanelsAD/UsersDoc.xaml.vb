﻿Imports MySql.Data.MySqlClient
Imports System.Data

Class UsersDoc

    Dim conexion As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable

    Private Sub Doctorusers_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles Doctorusers.SelectedCellsChanged
        Dim selectRow As DataRowView
        selectRow = Doctorusers.SelectedItem
        CodClinica.Text = selectRow.Row.ItemArray(1).ToString
        Nameuser.Text = selectRow.Row.ItemArray(4).ToString
        Firstuser.Text = selectRow.Row.ItemArray(5).ToString
        user.Text = selectRow.Row.ItemArray(2).ToString
        Txt_newpassword.Text = selectRow.Row.ItemArray(3).ToString
        IDnew.Text = selectRow.Row.ItemArray(0).ToString
        tel1.Text = selectRow.Row.ItemArray(6).ToString
        tel2.Text = selectRow.Row.ItemArray(7).ToString
        tel3.Text = selectRow.Row.ItemArray(8).ToString
    End Sub

    Private Sub UsersDoc_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Usuarios()
    End Sub

    Private Sub Usuarios()
        Try
            conexion.Open()
            consulta = "SELECT idusuario, codclinica, user, pass, docnom, apnom, tel1, tel2, tel3 FROM usuarios WHERE tipo_usuario='Doctor'"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            adaptador.Fill(tabla)
            Doctorusers.ItemsSource = tabla.DefaultView
        Catch ex As MySqlException
            MsgBox(ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub
End Class
