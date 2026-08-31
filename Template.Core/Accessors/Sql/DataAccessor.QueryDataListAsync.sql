SELECT * FROM Data
WHERE (/*@ flag */0 IS NULL) OR (Flag = /*@ flag */0)
ORDER BY Id
OFFSET /*@ offset */0 ROWS FETCH NEXT /*@ limit */10 ROWS ONLY
