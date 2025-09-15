use BookStoreDb

go
INSERT INTO Authors (FullName)
VALUES 
(N'J.K. Rowling'),
(N'George R.R. Martin'),
(N'Haruki Murakami'),
(N'Nguyễn Nhật Ánh'),
(N'Paulo Coelho');
go
INSERT INTO Publishers (Name)
VALUES
(N'Bloomsbury Publishing'),   -- Id = 1
(N'Bantam Books'),            -- Id = 2
(N'HarperCollins');           -- Id = 3

select * from Publishers

go
INSERT INTO Books 
    (Title, Description, Price, IsRead, DateRead, Rate, Genre, CoverUrl, DateAdded, PublisherId)
VALUES
    (
        N'Cho Tôi Xin Một Vé Đi Tuổi Thơ', 
        N'Cuốn sách kể về tuổi thơ ngây thơ và những kỷ niệm đẹp.', 
        10.50, 
        1,  -- IsRead = true (1) hoặc false (0)
        '2023-01-15',  -- DateRead
        5,  -- Rate
        N'Văn học thiếu nhi', 
        N'https://example.com/cover1.jpg', 
        GETDATE(),  -- DateAdded là ngày hiện tại
        1  -- PublisherId
    ),
    (
        N'Harry Potter and the Sorcerer’s Stone',
        N'Cuốn sách đầu tiên trong loạt truyện Harry Potter.',
        20.00,
        0,  -- chưa đọc
        NULL,  -- DateRead chưa có
        NULL,  -- Rate chưa đánh giá
        N'Fantasy',
        N'https://example.com/cover2.jpg',
        GETDATE(),
        2
    ),
    (
        N'A Game of Thrones',
        N'Cuốn sách về những cuộc chiến tranh giành ngai vàng.',
        22.00,
        1,
        '2024-05-20',
        4,
        N'Fantasy',
        N'https://example.com/cover3.jpg',
        GETDATE(),
        3
    );

	