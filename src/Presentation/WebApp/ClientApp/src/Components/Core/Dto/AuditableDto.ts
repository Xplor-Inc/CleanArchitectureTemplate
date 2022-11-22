export interface IAuditableDto {
    id?: number
    uniqueId?: string
    createdById?: string
    createdOn?: Date
    deletedById?: string
    deletedOn?: Date
    updatedById?: string
    updatedOn?: Date
}