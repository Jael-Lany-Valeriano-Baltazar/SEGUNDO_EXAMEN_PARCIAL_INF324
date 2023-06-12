/****** Script para el comando SelectTopNRows de SSMS  ******/
SELECT TOP (1000) [ID]
      ,[NOM_COLOR]
      ,[R1]
      ,[G1]
      ,[B1]
  FROM [DB_TEXTURA].[dbo].[RGB_COLOR];

  DELETE TOP(17)
  FROM RGB_COLOR;