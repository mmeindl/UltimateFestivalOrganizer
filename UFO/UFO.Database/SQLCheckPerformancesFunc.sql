ALTER FUNCTION [dbo].[checkPerformances] (
  @artistId INT, @dt DATETIME
)
RETURNS BIT
AS
BEGIN
  DECLARE @res BIT;
  SET @res = 1;
  IF EXISTS (SELECT* FROM Performance
         WHERE Performance.artistId = @artistId AND (
         Performance.dateTime = DATEADD(hour, 1, @dt) OR
         Performance.dateTime = DATEADD(hour, -1, @dt) OR
		 Performance.dateTime = @dt))
    SET @res = 0; /* insertion not possible */
  return @res; /* insertion possible */
END;