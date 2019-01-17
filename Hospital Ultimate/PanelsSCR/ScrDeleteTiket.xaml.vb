Imports MySql.Data.MySqlClient
Imports System.Data

Public Class ScrDeleteTiket

    Dim fila As Integer = -1
    Dim row As DataRowView
    Dim conexion As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable
    Dim eliminar As String = String.Empty
    Dim vaciar As String = String.Empty

    Private Sub Lista()
        Try
            conexion.Open()
            consulta = "SELECT * FROM pacientes"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            tabla.Clear()
            adaptador.Fill(tabla)
            DtTikets.ItemsSource = tabla.DefaultView
        Catch ex As Exception
            MsgBox(ex.Message)
            Log.e("Error con excepción y traza", ex, New StackFrame(True))
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub ScrDeleteTiket_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Lista()
    End Sub

    Private Sub DtTikets_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles DtTikets.SelectedCellsChanged
        If DtTikets.SelectedIndex <> -1 Then
            fila = DtTikets.SelectedIndex
            row = DtTikets.SelectedItem
            idcliente.Text = row.Row.ItemArray(0).ToString
            idDoctor.Text = row.Row.ItemArray(1).ToString
            doctor.Text = row.Row.ItemArray(2).ToString
            tiket.Text = row.Row.ItemArray(3).ToString
            clinica.Text = row.Row.ItemArray(4).ToString
            estado.Text = row.Row.ItemArray(5).ToString
        Else
            fila = -1
            idcliente.Text = ""
            idDoctor.Text = ""
            doctor.Text = ""
            tiket.Text = ""
            clinica.Text = ""
            estado.Text = ""
            row.CancelEdit()
        End If
    End Sub

    Private Sub DeleteTiket_Click(sender As Object, e As RoutedEventArgs) Handles DeleteTiket.Click
        If DtTikets.SelectedIndex <> -1 Then
            Delete()
            DtTikets.SelectedIndex = fila
            Lista()
        End If
    End Sub

    Private Sub Delete()
        Try
            conexion.Open()
            eliminar = "DELETE FROM pacientes WHERE pacientes.IDcliente='" & idcliente.Text & "'"
            comando = New MySqlCommand(eliminar, conexion)
            comando.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
            Log.e("Error con excepción y traza", ex, New StackFrame(True))
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub vaciarlista_Click(sender As Object, e As RoutedEventArgs) Handles vaciarlista.Click
        Try
            conexion.Open()
            vaciar = "TRUNCATE TABLE pacientes"
            comando = New MySqlCommand(vaciar, conexion)
            comando.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
            Log.e("Error con excepción y Traza", ex, New StackFrame(True))
        Finally
            conexion.Close()
        End Try
    End Sub
End Class
