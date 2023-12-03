import type { Response } from './response';

export interface Method {
    method_id: string | null;
    name: string;
    method_type: string;
    responses?: Response[];
}