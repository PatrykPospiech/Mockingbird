import type { ApiResource } from './api_resources';
import type { Option } from './option';

export interface CarrierData {
    carrier_id: string;
    name: string;
    nickname:string;
    icon?: object;
    options?: Option[];
    api_resources?: ApiResource[];
}