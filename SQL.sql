USE sp_base
GO

ALTER VIEW dbo.v_PayDoc
AS
SELECT
  pd.PayDocId
 ,pd.DocType
 ,pd.ExDocType
 ,pd.OnDate
 ,pd.KaId
 ,pd.Total
 ,pd.PTypeId
 ,pd.CurrId
 ,pd.Deleted
 ,pd.DocNum
 ,pd.Checked
 ,pd.WithNDS
 ,pd.Reason
 ,pd.Notes
 ,pd.MPersonId
 ,pd.CTypeId
 ,pd.AccId
 ,pd.CashId
 ,pd.OperId
 ,pd.DocId
 ,pd.OnValue
 ,pd.Schet
 ,c.ShortName AS CurrName
 ,ka.Name AS KaName
 ,ka.FullName AS KaFullName
 ,ka.Phone AS KaPhone
 ,kaemp.Name AS PersonName
 ,kadd.Address
 ,kadd.City
 ,kadd.District
 ,kadd.Country
 ,kadd.PostIndex
 ,ka.Fax
 ,ka.Email
 ,ka.www
 ,ka.KType
 ,ch.Name AS ChargeName
 ,ptype.Name AS PayTypeName
 ,cashd.Name AS CashdName
 ,ACC.AccNum
 ,b.Name AS BankName
 ,ka.KAU
 ,pd.Id
 ,pd.SummInCurr
 ,pd.ActualSummInCurr
 ,ACC_B.AccNum AS KaAccNum
 ,pd.BankCommission
 ,pd.EntId
 ,ent.Name AS EntName
 ,kg.Name AS KaGroupName
 ,dt.Name AS DocTypeName
 ,COALESCE(cashd.Name, ACC.AccNum) AS SourceType
FROM dbo.PayDoc AS pd
LEFT OUTER JOIN dbo.Kagent AS ka
  ON ka.KaId = pd.KaId
LEFT OUTER JOIN dbo.Currency AS c
  ON c.CurrId = pd.CurrId
LEFT OUTER JOIN dbo.Kagent AS kaemp
  ON pd.MPersonId = kaemp.KaId
LEFT OUTER JOIN dbo.KaAddr AS kadd
  ON kadd.KaId = kaemp.KaId
    AND kadd.AddrType = 0
LEFT OUTER JOIN dbo.ChargeType AS ch
  ON ch.CTypeId = pd.CTypeId
LEFT OUTER JOIN dbo.PayType AS ptype
  ON ptype.PTypeId = pd.PTypeId
LEFT OUTER JOIN dbo.CashDesks AS cashd
  ON cashd.CashId = pd.CashId
LEFT OUTER JOIN dbo.KAgentAccount AS ACC
  ON ACC.AccId = pd.AccId
LEFT OUTER JOIN dbo.Banks AS b
  ON b.BankId = ACC.BankId
LEFT OUTER JOIN dbo.KAgentAccount AS ACC_B
  ON ACC_B.AccId = pd.KaAccId
LEFT OUTER JOIN dbo.Kagent AS ent
  ON pd.EntId = ent.KaId
LEFT OUTER JOIN dbo.KontragentGroup AS kg
  ON kg.Id = ka.GroupId
LEFT OUTER JOIN dbo.DocType AS dt
  ON dt.Id = pd.ExDocType
GO

EXEC sys.sp_addextendedproperty N'MS_DiagramPane1'
                               ,N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[48] 4[16] 2[27] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pd"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 22
         End
         Begin Table = "ka"
            Begin Extent = 
               Top = 6
               Left = 250
               Bottom = 136
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 462
               Bottom = 136
               Right = 636
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "kaemp"
            Begin Extent = 
               Top = 6
               Left = 674
               Bottom = 136
               Right = 848
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "kadd"
            Begin Extent = 
               Top = 6
               Left = 886
               Bottom = 136
               Right = 1060
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ch"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 251
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ptype"
            Begin Extent = 
               Top = 138
               Left = 250
               Bottom = 234
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 0
   '
                               ,'SCHEMA'
                               ,N'dbo'
                               ,'VIEW'
                               ,N'v_PayDoc'
GO

EXEC sys.sp_addextendedproperty N'MS_DiagramPane2'
                               ,N'      End
         Begin Table = "cashd"
            Begin Extent = 
               Top = 138
               Left = 462
               Bottom = 268
               Right = 636
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ACC"
            Begin Extent = 
               Top = 138
               Left = 674
               Bottom = 268
               Right = 848
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 138
               Left = 886
               Bottom = 268
               Right = 1060
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ACC_B"
            Begin Extent = 
               Top = 256
               Left = 38
               Bottom = 386
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ent"
            Begin Extent = 
               Top = 234
               Left = 250
               Bottom = 364
               Right = 435
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "kg"
            Begin Extent = 
               Top = 366
               Left = 250
               Bottom = 462
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dt"
            Begin Extent = 
               Top = 390
               Left = 38
               Bottom = 520
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 42
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 3330
         Table = 1845
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
'
                               ,'SCHEMA'
                               ,N'dbo'
                               ,'VIEW'
                               ,N'v_PayDoc'
GO

EXEC sys.sp_addextendedproperty N'MS_DiagramPaneCount'
                               ,2
                               ,'SCHEMA'
                               ,N'dbo'
                               ,'VIEW'
                               ,N'v_PayDoc'
GO