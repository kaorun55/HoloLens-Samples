using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_UWP
using Windows.Storage;
using System.Threading.Tasks;
#endif

public class DocumentLibraryTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if UNITY_UWP
        Task.Run(async ()=>
        {

            // ドキュメントフォルダ
            // 1. Package.appxmanifestを開き
            //    「宣言」から「ファイルの種類の関連付け」を行い、書き込むファイルの拡張を追加する
            // 2. Package.appxmanifestを「コードで開く」で開き下記を追加する
            //     <uap:Capability Name="documentsLibrary" />
            {
                var folder = await KnownFolders.DocumentsLibrary.CreateFolderAsync("DocumentLibraryTest", CreationCollisionOption.OpenIfExists);
                var file = await folder.CreateFileAsync("test.txt", CreationCollisionOption.ReplaceExisting);

                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    var bytes = System.Text.Encoding.UTF8.GetBytes(@"テストてすとtest");
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                }
            }

            // ローカルフォルダー
            // 「User Files\LocalAppData\<アプリ名>\LocalState」 以下にできる
            {
                var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("DocumentLibraryTest", CreationCollisionOption.OpenIfExists);
                var file = await folder.CreateFileAsync("test.txt", CreationCollisionOption.ReplaceExisting);

                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    var bytes = System.Text.Encoding.UTF8.GetBytes(@"テストてすとtest");
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                }
            }

            // カメラロール
            // ピクチャライブラリの権限が必要
            // フォルダは作れない
            {
                var file = await KnownFolders.CameraRoll.CreateFileAsync("test.txt", CreationCollisionOption.ReplaceExisting);

                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    var bytes = System.Text.Encoding.UTF8.GetBytes(@"テストてすとtest");
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                }
            }
        });
#endif
    }

    // Update is called once per frame
    void Update () {
		
	}
}
