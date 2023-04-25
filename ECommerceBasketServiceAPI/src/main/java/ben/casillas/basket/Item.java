package ben.casillas.basket;

import java.time.LocalDate;
import  org.springframework.data.annotation.*;
//import jakarta.persistence.*;
import java.io.Serializable;
import java.util.UUID;
public class Item implements Serializable {
    private static final long serialVersionUID = 1L;
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

    public void setUnitPrice(double unitPrice) {
        this.unitPrice = unitPrice;
    }
}
