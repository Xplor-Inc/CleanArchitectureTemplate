import { IAuditableDto } from "../AuditableDto";

export interface IChangeLogDto extends IAuditableDto {
    entityName: string
    newValue?: string
    oldValue?: string
    primaryKey: number
    propertyName: string
}