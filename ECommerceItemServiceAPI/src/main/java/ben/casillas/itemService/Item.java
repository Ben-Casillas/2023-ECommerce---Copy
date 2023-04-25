package ben.casillas.itemService;

import org.springframework.data.annotation.Id;
import java.util.UUID;

public class Item {
   @Id
    private UUID itemUUID;
   private String title;
   private String description;

   private double unitPrice;

    public UUID getItemUUID() {
        return itemUUID;
    }

    public void setItemUUID(UUID itemUUID) {
        this.itemUUID = itemUUID;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public double getUnitPrice() {
        return unitPrice;
    }
}
