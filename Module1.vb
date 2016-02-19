Module Module1
	Dim pokename_list As New List(Of String)

	Dim tail_cache() As String
	Dim head_cache() As String

	Sub Main()
		Using sr As New IO.StreamReader("data.txt", System.Text.Encoding.UTF8)
			While Not sr.EndOfStream
				pokename_list.Add(sr.ReadLine())
			End While
		End Using

		ReDim tail_cache(pokename_list.Count - 1)
		ReDim head_cache(pokename_list.Count - 1)

		For i = 0 To pokename_list.Count - 1
			Dim nowstr As String = pokename_list(i)
			head_cache(i) = nowstr.Substring(0, 1)
			If nowstr.Last = "ー" Then
				tail_cache(i) = nowstr.Substring(nowstr.Count - 1 - 1, 1)
			Else
				tail_cache(i) = nowstr.Last
			End If
		Next

		For i = 0 To pokename_list.Count - 1
			Dim combination(pokename_list.Count - 1) As Boolean
			Dim combination_list As New List(Of Integer)
			search(i, 0, combination, combination_list)
			Console.WriteLine(i & "/" & pokename_list.Count - 1 & " " & i / pokename_list.Count & "%searched")
		Next
	End Sub

	Dim max_depth As Integer

	Sub search(pos As Integer, depth As Integer, combination() As Boolean, combination_list As List(Of Integer))
		combination(pos) = True
		combination_list.Add(pos)

		Dim notfound As Boolean = True

		For i = 0 To pokename_list.Count - 1
			If Not combination(i) Then
				If tail_cache(pos) = head_cache(i) Then
					notfound = False
					Dim newcombination() As Boolean = DirectCast(combination.Clone(), Boolean())
					Dim newcombination_list As New List(Of Integer)(combination_list)
					search(i, depth + 1, newcombination, newcombination_list)
				End If
			End If
		Next

		If notfound Then
			If depth > max_depth Then
				max_depth = depth
				For Each i In combination_list
					Console.Write(pokename_list(i) & "→")
				Next
				Console.WriteLine("")
			End If
		End If
	End Sub
End Module
