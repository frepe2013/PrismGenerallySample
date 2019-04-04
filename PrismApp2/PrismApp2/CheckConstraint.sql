ALTER TABLE [dbo].[Books]
    ADD CONSTRAINT [CK_Books_AuthorGender] CHECK ([AuthorGender]='F' OR [AuthorGender]='M' OR [AuthorGender]='O');
