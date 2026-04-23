import {getPool, sql} from "./db.mjs";

const ACTIVATE_DISCOUNT_SQL =
    `
        UPDATE p
        SET p.IsDiscountActive = 1 FROM Products p
        WHERE p.ISDiscountActive = 0
          AND p.DiscountStart IS NOT NULL
          AND p.DiscountEnd IS NOT NULL
          AND p.DiscountStart <= @nowUtc
          AND p.DiscountEnd >= @nowUtc
    `

const DEACTIVATE_DISCOUNT_SQL =
    `
        UPDATE p
        SET p.IsDiscountActive = 0 FROM Products p
        WHERE p.ISDiscountActive = 1
          AND (p.DiscountStart IS NULL
           OR p.DiscountEnd IS NULL
           OR p.DiscountStart
            > @nowUtc
           OR p.DiscountEnd
            < @nowUtc)
    `


