update tblLessonStudents set LessonId=3, StudentName='deneme' where LessonId=2 and StudentName='deneme 2'

declare @LessonId int
declare @OldLessonId int
declare @StudentName nvarchar(50)
declare @OldStudentName nvarchar(50)

select @LessonId=5, @OldLessonId =3, @StudentName=N'Yeni', @OldStudentName=N'ömer'

update tblLessonStudents set LessonId=@LessonId, StudentName=@StudentName 
where LessonId=@OldLessonId and StudentName=@OldStudentName