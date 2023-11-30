import type { Header } from './header';

export interface Response {
    response_id: string;
    is_active: boolean;
    response_status_code: string;
    response_body: string;
    headers?: Header[];

}