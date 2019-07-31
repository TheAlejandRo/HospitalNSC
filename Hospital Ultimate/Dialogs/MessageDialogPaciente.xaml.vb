Imports System.Speech.Synthesis

Public Class MessageDialogPaciente

    Dim voz As New SpeechSynthesizer
    Dim WithEvents tiemp As New Threading.DispatcherTimer
    Dim tiempejec As Integer = 0

    Private Sub MessageDialogPaciente_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        tiempejec = 0
        tiemp.Interval = New TimeSpan(0, 0, 1)
        tiemp.Start()
    End Sub

    Private Sub SpeakVoice()
        voz.Volume = 100
        voz.Rate = -2
        voz.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, New Globalization.CultureInfo("es-ES"))
        voz.Speak("Ticket del paciente...")
        voz.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, New Globalization.CultureInfo("es-ES"))
        voz.Rate = -3
        voz.Speak(CodPac.Text)
        voz.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, New Globalization.CultureInfo("es-ES"))
        voz.Rate = -2
        voz.Speak("Le atenderán en la clínica DE...")
        voz.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 12, New Globalization.CultureInfo("es-ES"))
        voz.Rate = -3
        voz.Speak(Header.Content)
    End Sub

    Private Sub tiemp_Tick(sender As Object, e As EventArgs) Handles tiemp.Tick
        SpeakVoice()
        tiemp.Stop()
    End Sub
End Class
