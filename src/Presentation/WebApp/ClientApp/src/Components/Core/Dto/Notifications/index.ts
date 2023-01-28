import { IAuditableDto } from "../AuditableDto";

export interface INotificationDto extends IAuditableDto {
    message?: string
    expireAt?: Date | null
    startAt?: Date | null
    isPermanent?: boolean
}