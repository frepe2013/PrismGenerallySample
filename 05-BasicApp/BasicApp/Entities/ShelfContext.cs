namespace BasicApp.Entities
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ShelfContext : DbContext
    {
        // コンテキストは、アプリケーションの構成ファイル (App.config または Web.config) から 'ShelfContext' 
        // 接続文字列を使用するように構成されています。既定では、この接続文字列は LocalDb インスタンス上
        // の 'BasicApp.Entities.ShelfContext' データベースを対象としています。 
        // 
        // 別のデータベースとデータベース プロバイダーまたはそのいずれかを対象とする場合は、
        // アプリケーション構成ファイルで 'ShelfContext' 接続文字列を変更してください。
        public ShelfContext()
            : base("name=ShelfContext")
        {
            this.Database.Log = Console.WriteLine;
        }

        // モデルに含めるエンティティ型ごとに DbSet を追加します。Code First モデルの構成および使用の
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=390109 を参照してください。

        public virtual DbSet<Book> Books { get; set; }
    }
}