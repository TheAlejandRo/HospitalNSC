Imports System.Data
Imports MaterialDesignThemes.Wpf
Imports MySql.Data.MySqlClient

Public Class Scrpanel3

    Dim conexion As New MySqlConnection("host=127.0.0.1; user=root; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adapatador As MySqlDataAdapter
    Dim tabla As New DataTable

    Private Sub Scrpanel1_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Try
            conexion.Open()
            consulta = "SELECT idusuario, docnom, apnom FROM usuarios WHERE tipo_usuario='Doctor'"
            comando = New MySqlCommand(consulta, conexion)
            adapatador = New MySqlDataAdapter(comando)
            adapatador.Fill(tabla)
            If tabla.Rows(0)(0).ToString = "6" Then
                Drname1.Text = tabla.Rows(0)(1).ToString
                Drfristname1.Text = tabla.Rows(0)(2).ToString
            ElseIf tabla.Rows(0)(0).ToString = "7" Then
                Drname2.Text = tabla.Rows(0)(1).ToString
                Drfristname2.Text = tabla.Rows(0)(2).ToString
            ElseIf tabla.Rows(0)(0).ToString = "8" Then
                Drname3.Text = tabla.Rows(0)(1).ToString
                Drfristname3.Text = tabla.Rows(0)(2).ToString
            ElseIf tabla.Rows(0)(0).ToString = "9" Then
                Drname4.Text = tabla.Rows(0)(1).ToString
                Drfristname4.Text = tabla.Rows(0)(2).ToString
            ElseIf tabla.Rows(0)(0).ToString = "10" Then
                Drname5.Text = tabla.Rows(0)(1).ToString
                Drfristname5.Text = tabla.Rows(0)(2).ToString
            ElseIf tabla.Rows(0)(0).ToString = "11" Then
                Drname6.Text = tabla.Rows(0)(1).ToString
                Drfristname6.Text = tabla.Rows(0)(2).ToString
            ElseIf tabla.Rows(0)(0).ToString = "12" Then
                Drname7.Text = tabla.Rows(0)(1).ToString
                Drfristname7.Text = tabla.Rows(0)(2).ToString
            ElseIf tabla.Rows(0)(0).ToString = "13 " Then
                Drname8.Text = tabla.Rows(0)(1).ToString
                Drfristname8.Text = tabla.Rows(0)(2).ToString
            ElseIf tabla.Rows(0)(0).ToString = "14" Then
                Drname9.Text = tabla.Rows(0)(1).ToString
                Drfristname9.Text = tabla.Rows(0)(2).ToString
            ElseIf tabla.Rows(0)(0).ToString = "15" Then
                Drname10.Text = tabla.Rows(0)(1).ToString
                Drfristname10.Text = tabla.Rows(0)(2).ToString
            Else
                Dim dlgshw As New MessageDialog
                dlgshw.Message.Text = "No existe ningun Doctor aún"
                DialogHost.Show(dlgshw, "RootDialog")
            End If
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub
End Class
