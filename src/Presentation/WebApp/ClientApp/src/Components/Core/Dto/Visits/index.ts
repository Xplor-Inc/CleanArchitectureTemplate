import { IAuditableDto } from "../AuditableDto";

export interface CounterDto extends IAuditableDto {
    browser: string;
    device: string;
    ipAddress: string;
    lastVisit: Date | null;
    operatingSystem: string;
    page: string;
    search: string;
    visitorId: string;
    tracking: number;
    enquirySent: number;
}